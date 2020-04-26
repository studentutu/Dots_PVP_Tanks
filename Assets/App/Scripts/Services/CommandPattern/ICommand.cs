namespace Scripts.CommandPattern
{
    public interface ICommand
    {
        void Execute(IReceiver receiver);
    }
}
