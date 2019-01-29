namespace PassingStateViaStateHolderParameters
{
    public interface IStateSetter<T>
    {
        void SetValue(T value);
    }
}