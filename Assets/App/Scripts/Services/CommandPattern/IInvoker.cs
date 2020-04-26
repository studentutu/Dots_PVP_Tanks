namespace Scripts.CommandPattern
{
    public interface IInvoker
    {
        void InvokeCommand(ICommand command);
    }

}