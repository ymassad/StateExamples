namespace MultithreadingAndRefStateParametersAndStateHolder
{
    public interface IStateGetter<TState>
    {
        TState GetState();
    }
}