using Essentia.Reflection;
using System;
using System.Text;
using UnityEngine;

namespace Essentia
{
	public static class Console
	{
		public static void Log(object @object, string moduleName = null, string typeName = null) =>
			Debug.Log(CreateCompositeMessage(@object.ToString(), moduleName, typeName));

		public static void Log(string message, string moduleName = null, string typeName = null) =>
			Debug.Log(CreateCompositeMessage(message, moduleName, typeName));

		public static void LogWarning(string message, string moduleName = null, string typeName = null) =>
			Debug.LogWarning(CreateCompositeMessage(message, moduleName, typeName));

		public static void LogError(string message, string moduleName = null, string typeName = null) =>
			Debug.LogError(CreateCompositeMessage(message, moduleName, typeName));

		public static void Log<T>(object @object, bool isModuleName = true, bool isTypeName = true) =>
			Debug.Log(CreateCompositeMessage<T>(@object.ToString(), isModuleName, isTypeName));

		public static void Log<T>(string message, bool isModuleName = true, bool isTypeName = true) =>
			Debug.Log(CreateCompositeMessage<T>(message, isModuleName, isTypeName));

		public static void LogWarning<T>(string message, bool isModuleName = true, bool isTypeName = true) =>
			Debug.LogWarning(CreateCompositeMessage<T>(message, isModuleName, isTypeName));

		public static void LogError<T>(string message, bool isModuleName = true, bool isTypeName = true) =>
			Debug.LogError(CreateCompositeMessage<T>(message, isModuleName, isTypeName));

		private static string CreateCompositeMessage<T>(string message, bool isModuleName, bool isTypeName)
		{
			Type type = typeof(T);
			string typeName = isTypeName ? type.Name : null;
			string moduleName = isModuleName ? Metadata.GetModuleName(type) : null;

			return CreateCompositeMessage(message, moduleName, typeName);
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
