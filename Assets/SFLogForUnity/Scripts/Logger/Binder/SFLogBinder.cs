using UnityEngine;

namespace SFLogForUnity.Scripts.Logger.Log
{
    public static class SFLogBinder
    {
        public static void BindLogger(ILogger logger)
        {
            Debug.unityLogger.logHandler = logger;
        }
    }
}