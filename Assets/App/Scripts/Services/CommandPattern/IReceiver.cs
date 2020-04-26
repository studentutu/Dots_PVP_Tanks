namespace Scripts.CommandPattern
{
    public interface IReceiver
    {
        void IReceiveCommand(ICommand command);
    }

}