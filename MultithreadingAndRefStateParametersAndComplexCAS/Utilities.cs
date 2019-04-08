using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultithreadingAndRefStateParametersAndComplexCAS
{
    public static class Utilities
    {
        public static void UpdateViaCAS<TState>(ref TState state, Func<TState, TState> update) where TState : class
        {
            var spinWait = new SpinWait();

            while (true)
            {
                TState beforeUpdate = state;

                TState updatedValue = update(beforeUpdate);

                TState found = Interlocked.CompareExchange(
                    location1: ref state,
                    value: updatedValue,
                    comparand: beforeUpdate);

                if (beforeUpdate == found)
                    return;

                spinWait.SpinOnce();
            }
        }
    }
}
