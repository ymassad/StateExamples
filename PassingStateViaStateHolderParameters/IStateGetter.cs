namespace PassingStateViaStateHolderParameters
{
    public interface IStateGetter<T>
    {
        T GetValue();
    }
}