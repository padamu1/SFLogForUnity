using System.Collections.Generic;
using System.Collections.Concurrent;

namespace SFLogForUnity.Scripts.Core.Pool
{
    public abstract class Pool<T> : IPool<T> where T : IPoolObject
    {
        ConcurrentQueue<T> queue = new ConcurrentQueue<T>();

        List<T> rentList = new List<T>();

        int poolMaxCount;
        int objectCount;

        protected Pool(int poolMaxCount)
        {
            this.poolMaxCount = poolMaxCount;
        }

        public bool Add()
        {
            if(objectCount > poolMaxCount)
            {
                return false;
            }

            objectCount++;
            queue.Enqueue(AddObject());

            return true;
        }

        protected abstract T AddObject();

        public T Rent()
        {
            lock (rentList)
            {
                if (queue.TryDequeue(out T poolObject))
                {
                    rentList.Add(poolObject);

                    poolObject.SetRent();

                    return poolObject;
                }

                if (Add())
                {
                    return Rent();
                }

                if (rentList.Count == 0)
                {
                    poolObject = rentList[0];
                    rentList.RemoveAt(0);
                    poolObject.SetReturn();
                    poolObject.SetRent();

                    return poolObject;
                }

                return default(T);
            }
        }

        public void ReturnToPool(T t)
        {
            lock (rentList)
            {
                t.SetReturn();
                rentList.Remove(t);
                queue.Enqueue(t);
            }
        }
    }
}
