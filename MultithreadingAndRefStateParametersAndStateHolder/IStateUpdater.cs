using System;

namespace MultithreadingAndRefStateParametersAndStateHolder
{
    public interface IStateUpdater<TState>
    {
        void UpdateState(Func<TState, TState> updateFunction);
    }
}