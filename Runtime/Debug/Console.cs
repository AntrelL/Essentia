using System.Text;
using UnityEngine;

namespace Essentia
{
    public static class Console
    {
        #region Log Methods
        public static void Log(object @object, string moduleName = null, string typeName = null) =>
            Debug.Log(CreateCompositeMessage(@object.ToString(), moduleName, typeName));

        public static void Log(string message, string moduleName = null, string typeName = null) =>
            Debug.Log(CreateCompositeMessage(message, moduleName, typeName));

        public static void Log(object @object, ConsoleOutputConfig outputConfig) =>
            Debug.Log(CreateCompositeMessage(@object.ToString(), outputConfig));

        public static void Log(string message, ConsoleOutputConfig outputConfig) =>
            Debug.Log(CreateCompositeMessage(message, outputConfig));

        public static void Log<T>(object @object, bool isModuleName = true, bool isTypeName = true) =>
            Debug.Log(CreateCompositeMessage<T>(@object.ToString(), isModuleName, isTypeName));

        public static void Log<T>(string message, bool isModuleName = true, bool isTypeName = true) =>
            Debug.Log(CreateCompositeMessage<T>(message, isModuleName, isTypeName));
        #endregion

        #region Log Warning Methods
        public static void LogWarning(string message, string moduleName = null, string typeName = null) =>
            Debug.LogWarning(CreateCompositeMessage(message, moduleName, typeName));

        public static void LogWarning(string message, ConsoleOutputConfig outputConfig) =>
            Debug.LogWarning(CreateCompositeMessage(message, outputConfig));

        public static void LogWarning<T>(string message, bool isModuleName = true, bool isTypeName = true) =>
            Debug.LogWarning(CreateCompositeMessage<T>(message, isModuleName, isTypeName));
        #endregion

        #region Log Error Methods
        public static void LogError(string message, string moduleName = null, string typeName = null) =>
            Debug.LogError(CreateCompositeMessage(message, moduleName, typeName));

        public static void LogError(string message, ConsoleOutputConfig outputConfig) =>
            Debug.LogError(CreateCompositeMessage(message, outputConfig));

        public static void LogError<T>(string message, bool isModuleName = true, bool isTypeName = true) =>
            Debug.LogError(CreateCompositeMessage<T>(message, isModuleName, isTypeName));
        #endregion

        private static string CreateCompositeMessage<T>(string message, bool isModuleName, bool isTypeName)
        {
            return CreateCompositeMessage(message, new ConsoleOutputConfig(typeof(T), isModuleName, isTypeName));
        }

        private static string CreateCompositeMessage(string message, ConsoleOutputConfig outputConfig)
        {
            return CreateCompositeMessage(message, outputConfig.ModuleName, outputConfig.TypeName);
        }

        private static string CreateCompositeMessage(string message, string moduleName, string typeName)
        {
            bool isModuleName = string.IsNullOrEmpty(moduleName) == false;
            bool isTypeName = string.IsNullOrEmpty(typeName) == false;

            if ((isModuleName || isTypeName) == false)
                return message;

            StringBuilder completedMessage = new();

            completedMessage.Append(isModuleName ? $"[{moduleName}]" : string.Empty);
            completedMessage.Append(isTypeName ? " " + typeName : string.Empty);

            completedMessage.Append(completedMessage.Length > 0 ? ": " : string.Empty);
            completedMessage.Append(message);

            return completedMessage.ToString().Trim();
        }
    }
}
