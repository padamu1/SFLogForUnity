using System;
using UnityEngine;
using SFLogForUnity.Scripts.Core.Pool;
using SFLogForUnity.Scripts.Logger.Log;
using UnityEditor.VersionControl;

namespace SFLogForUnity.Scripts.Logger.Viewer
{
    public class SFLogViewer : MonoBehaviour, ILogger
    {
        [SerializeField] private SFLog logPrefab;
        [SerializeField] private int poolMaxCount;
        [SerializeField] private Transform logTarget;

        private LogPool logPool;

        public ILogHandler logHandler { get; set; }
        public bool logEnabled { get; set; }
        public LogType filterLogType { get; set; }

        private void Awake()
        {
            if (logPrefab == null || logTarget == null)
            {
                Destroy(this.gameObject);
                if (logPrefab == null)
                {
                    throw new Exception("LogPrefab is null");
                }

                if (logTarget == null)
                {
                    throw new Exception("LogTarget is null");
                }
            }
            logHandler = (Debug.unityLogger as UnityEngine.Logger).logHandler;
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
            logHandler.LogFormat(logType, null, "{0}", message);

            SetLog(logType, message.ToString());
        }

        public void Log(LogType logType, object message, UnityEngine.Object context)
        {
            logHandler.LogFormat(logType, context, "{0}", message);

            SetLog(logType, message.ToString());
        }

        public void Log(LogType logType, string tag, object message)
        {
            logHandler.LogFormat(logType, null, "{0}", message);

            SetLog(logType, message.ToString());
        }

        public void Log(LogType logType, string tag, object message, UnityEngine.Object context)
        {
            logHandler.LogFormat(logType, context, "{0}", message);

            SetLog(logType, message.ToString());
        }

        public void Log(object message)
        {
            logHandler.LogFormat(LogType.Log, null, "{0}", message);

            SetLog(LogType.Log, message.ToString());
        }

        public void Log(string tag, object message)
        {
            logHandler.LogFormat(LogType.Log, null, "{0}", message);

            SetLog(LogType.Log, message.ToString());
        }

        public void Log(string tag, object message, UnityEngine.Object context)
        {
            logHandler.LogFormat(LogType.Log, context, "{0}", message);

            SetLog(LogType.Log, message.ToString());
        }

        public void LogError(string tag, object message)
        {
            logHandler.LogFormat(LogType.Error, null, "{0}", message);

            SetLog(LogType.Error, message.ToString());
        }

        public void LogError(string tag, object message, UnityEngine.Object context)
        {
            logHandler.LogFormat(LogType.Error, context, "{0}", message);

            SetLog(LogType.Error, message.ToString());
        }

        public void LogException(Exception exception)
        {
            logHandler.LogException(exception, null);

            SetLog(LogType.Exception, exception.ToString());
        }

        public void LogException(Exception exception, UnityEngine.Object context)
        {
            logHandler.LogException(exception, context);

            SetLog(LogType.Exception, exception.ToString());
        }

        public void LogFormat(LogType logType, string format, params object[] args)
        {
            logHandler.LogFormat(logType, null, format, args);

            SetLog(logType, string.Format(format, args));
        }

        public void LogFormat(LogType logType, UnityEngine.Object context, string format, params object[] args)
        {
            logHandler.LogFormat(logType, context, format, args);

            SetLog(logType, string.Format(format, args));
        }

        public void LogWarning(string tag, object message)
        {
            logHandler.LogFormat(LogType.Warning, null, "{0}", message);

            SetLog(LogType.Warning, message.ToString());
        }

        public void LogWarning(string tag, object message, UnityEngine.Object context)
        {
            logHandler.LogFormat(LogType.Warning, context, "{0}", message);

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

            switch (logType)
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