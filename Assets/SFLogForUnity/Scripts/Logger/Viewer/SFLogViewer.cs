using System;
using UnityEngine;
using SFLogForUnity.Scripts.Core.Pool;
using SFLogForUnity.Scripts.Logger.Log;

namespace SFLogForUnity.Scripts.Logger.Viewer
{
    public class SFLogViewer : MonoBehaviour, ILogger
    {
        [SerializeField] private SFLog logPrefab;
        [SerializeField] private int poolMaxCount;
        [SerializeField] private Transform logTarget;

        ILogger unityLogger;

        private LogPool logPool;

        public ILogHandler logHandler { get; set; }
        public bool logEnabled { get; set; }
        public LogType filterLogType { get; set; }

        private void Awake()
        {
            if(logPrefab == null || logTarget == null)
            {
                Destroy(this.gameObject);
                if(logPrefab == null)
                {
                    throw new Exception("LogPrefab is null");
                }

                if(logTarget == null)
                {
                    throw new Exception("LogTarget is null");
                }
            }

            unityLogger = Debug.unityLogger;
        }

        private void Start()
        {
            logPool = LogPool.Create(logPrefab, poolMaxCount);
            SFLogBinder.BindLogger(this);
        }

        public bool IsLogTypeAllowed(LogType logType)
        {
            return logType != LogType.Assert;
        }

        public void Log(LogType logType, object message)
        {
            unityLogger?.Log(logType, message);

            SetLog(logType, message.ToString());
        }

        public void Log(LogType logType, object message, UnityEngine.Object context)
        {
            unityLogger?.Log(logType, message, context);

            SetLog(logType, message.ToString());
        }

        public void Log(LogType logType, string tag, object message)
        {
            unityLogger?.Log(logType, tag, message);

            SetLog(logType, message.ToString());
        }

        public void Log(LogType logType, string tag, object message, UnityEngine.Object context)
        {
            unityLogger?.Log(logType, tag, message, context);

            SetLog(logType, message.ToString());
        }

        public void Log(object message)
        {
            unityLogger?.Log(message);

            SetLog(LogType.Log, message.ToString());
        }

        public void Log(string tag, object message)
        {
            unityLogger?.Log(tag, message);

            SetLog(LogType.Log, message.ToString());
        }

        public void Log(string tag, object message, UnityEngine.Object context)
        {
            unityLogger?.Log(tag, message, context);

            SetLog(LogType.Log, message.ToString());
        }

        public void LogError(string tag, object message)
        {
            unityLogger?.Log(tag, message);

            SetLog(LogType.Error, message.ToString());
        }

        public void LogError(string tag, object message, UnityEngine.Object context)
        {
            unityLogger?.LogError(tag, message, context);

            SetLog(LogType.Error, message.ToString());
        }

        public void LogException(Exception exception)
        {
            unityLogger?.LogException(exception);

            SetLog(LogType.Exception, exception.ToString());
        }

        public void LogException(Exception exception, UnityEngine.Object context)
        {
            unityLogger?.LogException(exception, context);

            SetLog(LogType.Exception, exception.ToString());
        }

        public void LogFormat(LogType logType, string format, params object[] args)
        {
            unityLogger?.LogFormat(logType, format, args);

            SetLog(logType, string.Format(format, args));
        }

        public void LogFormat(LogType logType, UnityEngine.Object context, string format, params object[] args)
        {
            unityLogger?.LogFormat(logType, context, format, args);

            SetLog(logType, string.Format(format,args));
        }

        public void LogWarning(string tag, object message)
        {
            unityLogger?.LogWarning(tag, message);

            SetLog(LogType.Warning, message.ToString());
        }

        public void LogWarning(string tag, object message, UnityEngine.Object context)
        {
            unityLogger?.LogWarning(tag, message, context);

            SetLog(LogType.Warning, message.ToString());
        }

        private void SetLog(LogType logType, string message)
        {
            if (IsLogTypeAllowed(logType) == false)
            {
                return;
            }

            var poolObject = logPool.Rent();

            if (poolObject == null)
            {
                return;
            }

            poolObject.transform.SetParent(logTarget, false);
            poolObject.transform.SetAsFirstSibling();

            switch(logType)
            {
                case LogType.Log:
                    poolObject.Log(message);
                    break;
                case LogType.Warning:
                    poolObject.Warning(message);
                    break;
                case LogType.Exception:
                    poolObject.Exception(message);
                    break;
                case LogType.Error:
                    poolObject.Error(message);
                    break;
            }
        }
    }
}