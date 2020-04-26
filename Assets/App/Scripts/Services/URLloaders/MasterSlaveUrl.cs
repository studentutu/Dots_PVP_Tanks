using System.Collections.Generic;

namespace Scripts.Services
{
    [System.Serializable]
    public class MasterSlaveUrl
    {
        public List<SlaveUrl> Urls;
    }

    [System.Serializable]
    public class SlaveUrl
    {
        public string Name;
        public string Url;
    }
}