using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultithreadingAndRefStateParametersAndStateHolder
{
    public sealed class ThreadSafeStateHolder<TState> : IStateHolder<TState>
    {
        private TState state;

        private object lockingObject = new object();

        public ThreadSafeStateHolder(TState initialState)
        {
            state = initialState;
        }

        public TState GetState()
        {
            lock (lockingObject)
            {
                return state;
            }
        }

        public void SetState(TState newState)
        {
            lock (lockingObject)
            {
                state = newState;
            }
        }

        public void UpdateState(Func<TState, TState> updateFunction)
        {
            lock (lockingObject)
            {
                state = updateFunction(state);
            }
        }
    }
}
