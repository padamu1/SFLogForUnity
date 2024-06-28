using UnityEngine;
using SFLogForUnity.Scripts.Logger.Log;

namespace SFLogForUnity.Scripts.Core.Pool
{
    public class LogPool : Pool<SFLog>
    {
        SFLog prefab;

        protected LogPool(SFLog prefab, int poolMaxCount) : base(poolMaxCount)
        {
            this.prefab = prefab;
        }

        protected override SFLog AddObject()
        {
            return GameObject.Instantiate(prefab);
        }

        public static LogPool Create(SFLog prefab, int poolMaxCount)
        {
            return new LogPool(prefab, poolMaxCount);
        }
    }
}
