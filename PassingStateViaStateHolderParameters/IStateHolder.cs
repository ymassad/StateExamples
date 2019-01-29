namespace PassingStateViaStateHolderParameters
{
    public interface IStateHolder<T> : IStateGetter<T>, IStateSetter<T>
    {

    }
}