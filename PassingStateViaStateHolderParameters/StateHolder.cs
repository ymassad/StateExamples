namespace PassingStateViaStateHolderParameters
{
    public class StateHolder<T> : IStateHolder<T>
    {
        private T state;

        public StateHolder(T initialState)
        {
            state = initialState;
        }

        public void SetValue(T value) => state = value;

        public T GetValue() => state;
    }
}