namespace SFLogForUnity.Scripts.Core.Pool
{
    public interface IPool<T>
    {
        public bool Add();
        public T Rent();
        public void ReturnToPool(T t);

    }
}
