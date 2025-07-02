using UnityEngine;

public class CommandLogHandler : ILogHandler
{
    private static CommandLogHandler _instance;
    public static CommandLogHandler Instance => _instance ?? (_instance = new CommandLogHandler());

    private ILogHandler defaultLogHandler = Debug.unityLogger.logHandler;

    public void HandleLog(string condition, string stackTrace, LogType type)
    {
        defaultLogHandler.LogFormat(type, null, condition);
    }

    public void LogFormat(LogType logType, Object context, string format, params object[] args)
    {
        defaultLogHandler.LogFormat(logType, context, format, args);
    }

    public void LogException(System.Exception exception, Object context)
    {
        defaultLogHandler.LogException(exception, context);
    }
}
