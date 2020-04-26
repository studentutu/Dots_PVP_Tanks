namespace Scripts.Utils
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public class RenderTextureMeshCutter
    {
        public class MeshData
        {
            public Camera Camera;
            public List<int> Polygons;
            public MeshFilter Filter;
            public MeshFilter PolygonFilter;
            public float ScreenWidth;
            public float ScreenHeight;
            public RenderTexture RenderTexture;
            public Texture2D ScreenShot;
        }
        // .....................

        // Точка входа
        // Убираем из списка видимые полигоны, таким образом они не будут удалены впоследствии
        public static void SaveVisiblePolygons(MeshData data)
        {
            if (data is null)
            {
                throw new System.ArgumentNullException(nameof(data));
            }

            var polygonsCount = data.Polygons.Count;

            for (int i = polygonsCount - 1; i >= 0; i--)
            {
                var polygonId = data.Polygons[i];
                var worldVertices = GetPolygonWorldPositions(data.Filter, polygonId, data.PolygonFilter);
                var screenVertices = GetScreenVertices(worldVertices, data.Camera);
                screenVertices = ClampScreenCordinatesInViewPort(screenVertices, data.ScreenWidth, data.ScreenHeight);

                var gui0 = ConvertScreenToGui(screenVertices[0], data.ScreenHeight);
                var gui1 = ConvertScreenToGui(screenVertices[1], data.ScreenHeight);
                var gui2 = ConvertScreenToGui(screenVertices[2], data.ScreenHeight);
                var guiVertices = new[] { gui0, gui1, gui2 };

                var renderTextureRect = GetPolygonRect(guiVertices);
                if (renderTextureRect.width == 0 || renderTextureRect.height == 0) continue;

                var oldTriangles = data.Filter.sharedMesh.triangles;
                RemoveTrianglesOfPolygon(polygonId, data.Filter);

                var tex = GetTexture2DFromRenderTexture(renderTextureRect, data);

                // Если полигон виден (найден красный пиксель), то удаляем его из списка полигонов, которые необходимо удалить
                if (ThereIsPixelOfAColor(tex, renderTextureRect))
                {
                    data.Polygons.RemoveAt(i);
                }

                // Возвращаем проверяемый меш к исходному состоянию
                data.Filter.sharedMesh.triangles = oldTriangles;
            }
        }

        // Обрезаем координаты, чтобы не залезть за пределы рендер текстуры
        private static Vector3[] ClampScreenCordinatesInViewPort(Vector3[] screenPositions, float screenWidth, float screenHeight)
        {
            var len = screenPositions.Length;
            for (int i = 0; i < len; i++)
            {
                if (screenPositions[i].x < 0)
                {
                    screenPositions[i].x = 0;
                }
                else if (screenPositions[i].x >= screenWidth)
                {
                    screenPositions[i].x = screenWidth - 1;
                }

                if (screenPositions[i].y < 0)
                {
                    screenPositions[i].y = 0;
                }
                else if (screenPositions[i].y >= screenHeight)
                {
                    screenPositions[i].y = screenHeight - 1;
                }
            }

            return screenPositions;
        }

        // Возвращаем мировые координаты
        private static Vector3[] GetPolygonWorldPositions(MeshFilter filter, int polygonId, MeshFilter polygonFilter)
        {
            var sharedMesh = filter.sharedMesh;
            var meshTransform = filter.transform;
            polygonFilter.transform.position = meshTransform.position;

            var triangles = sharedMesh.triangles;
            var vertices = sharedMesh.vertices;

            var index = polygonId * 3;

            var localV0Pos = vertices[triangles[index]];
            var localV1Pos = vertices[triangles[index + 1]];
            var localV2Pos = vertices[triangles[index + 2]];

            var vertex0 = meshTransform.TransformPoint(localV0Pos);
            var vertex1 = meshTransform.TransformPoint(localV1Pos);
            var vertex2 = meshTransform.TransformPoint(localV2Pos);

            return new[] { vertex0, vertex1, vertex2 };
        }

        // Находим красный полигон
        private static bool ThereIsPixelOfAColor(Texture2D tex, Rect rect)
        {
            var width = (int)rect.width;
            var height = (int)rect.height;

            // Пиксели берутся из левого нижнего угла
            var pixels = tex.GetPixels(0, 0, width, height, 0);
            var len = pixels.Length;

            for (int i = 0; i < len; i += 1)
            {
                var pixel = pixels[i];
                if (pixel.r > 0f && pixel.g == 0 && pixel.b == 0 && pixel.a == 1) return true;
            }

            return false;
        }

        // Получаем фрагмент рендер текстуры по ректу
        private static Texture2D GetTexture2DFromRenderTexture(Rect renderTextureRect, MeshData data)
        {
            data.Camera.targetTexture = data.RenderTexture;
            data.Camera.Render();
            RenderTexture.active = data.Camera.targetTexture;

            data.ScreenShot.ReadPixels(renderTextureRect, 0, 0);

            RenderTexture.active = null;
            data.Camera.targetTexture = null;

            return data.ScreenShot;
        }

        // Удаляем треугольник с индексом polygonId из списка triangles
        private static void RemoveTrianglesOfPolygon(int polygonId, MeshFilter filter)
        {
            var triangles = filter.sharedMesh.triangles;
            var newTriangles = new int[triangles.Length - 3];
            var len = triangles.Length;

            var k = 0;
            for (int i = 0; i < len; i++)
            {
                var curPolygonId = i / 3;
                if (curPolygonId == polygonId) continue;

                newTriangles[k] = triangles[i];
                k++;
            }

            filter.sharedMesh.triangles = newTriangles;
        }

        // Переводим мировые в экранные координаты
        private static Vector3[] GetScreenVertices(Vector3[] worldVertices, Camera cam)
        {
            var scr0 = cam.WorldToScreenPoint(worldVertices[0]);
            var scr1 = cam.WorldToScreenPoint(worldVertices[1]);
            var scr2 = cam.WorldToScreenPoint(worldVertices[2]);
            return new[] { scr0, scr1, scr2 };
        }

        // Переводим экранные в Gui координаты
        private static Vector2 ConvertScreenToGui(Vector3 pos, float screenHeight)
        {
            return new Vector2(pos.x, screenHeight - pos.y);
        }

        // Вычисляем прямоугольник в Gui координатах
        private static Rect GetPolygonRect(Vector2[] guiVertices)
        {
            var minX = guiVertices.Min(v => v.x);
            var maxX = guiVertices.Max(v => v.x);

            var minY = guiVertices.Min(v => v.y);
            var maxY = guiVertices.Max(v => v.y);

            var width = Mathf.CeilToInt(maxX - minX);
            var height = Mathf.CeilToInt(maxY - minY);

            return new Rect(minX, minY, width, height);
        }
    }
}