namespace MultithreadingAndRefStateParametersAndStateHolder
{
    public interface IStateHolder<TState> : IStateUpdater<TState>, IStateGetter<TState>, IStateSetter<TState>
    {
    }
}