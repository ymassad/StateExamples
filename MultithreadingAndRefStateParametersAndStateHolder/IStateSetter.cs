namespace MultithreadingAndRefStateParametersAndStateHolder
{
    public interface IStateSetter<TState>
    {
        void SetState(TState newState);
    }
}