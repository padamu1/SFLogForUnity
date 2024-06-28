using SFLogForUnity.Scripts.Core.Pool;
using UnityEngine;

namespace SFLogForUnity.Scripts.Logger.Log
{
    public abstract class SFLog : MonoBehaviour, IPoolObject
    {
        public abstract void SetRent();

        public abstract void SetReturn();

        public void Error(string text)
        {
            Print($"<color=red>{text}</color>");
        }

        public void Exception(string text)
        {
            Print($"<color=red>{text}</color>");
        }

        public void Log(string text)
        {
            Print($"<color=white>{text}</color>");
        }


        public void Warning(string text)
        {
            Print($"<color=yellow>{text}</color>");
        }

        protected abstract void Print(string text);
    }
}
