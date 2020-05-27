﻿using System;
using WMI2CSharp.Models;

namespace WMI2CSharp.Log
{
    public static class LogEventHandler
    {
        public static event EventHandler<EventArgs<string>> OnConnectionMessage;
        public static event EventHandler<EventArgs<string>> OnDebugMessage;
        public static event EventHandler<EventArgs<string>> OnInformationMessage;
        public static event EventHandler<EventArgs<string>> OnWarningMessage;
        public static event EventHandler<EventArgs<string>> OnErrorMessage;
        public static event EventHandler<EventArgs<Exception>> OnExceptionMessage;

        internal static void Connection(string content)
        {
            AddLogMessage(OnConnectionMessage, content);
        }

        internal static void Debug(string content)
        {
            AddLogMessage(OnDebugMessage, content);
        }

        internal static void Information(string content)
        {
            AddLogMessage(OnInformationMessage, content);
        }

        internal static void Warning(string content)
        {
            AddLogMessage(OnWarningMessage, content);
        }

        internal static void Error(string content)
        {
            AddLogMessage(OnErrorMessage, content);
        }

        internal static void Exception(Exception exception)
        {
            AddLogMessage(OnExceptionMessage, exception);
        }

        private static void AddLogMessage<T>(EventHandler<EventArgs<T>> eventHandler, T content)
        {
            eventHandler?.Invoke(nameof(LogEventHandler), new EventArgs<T>(content));
        }
    }
}
