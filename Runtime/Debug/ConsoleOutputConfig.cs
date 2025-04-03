using Essentia.Reflection;
using System;

namespace Essentia
{
    public class ConsoleOutputConfig
    {
        public ConsoleOutputConfig(Type type, bool isModuleName, bool isTypeName)
        {
            ModuleName = isModuleName ? Metadata.GetModuleName(type) : null;
            TypeName = isTypeName ? type.Name : null;
        }

        public ConsoleOutputConfig(string moduleName, string typeName)
        {
            ModuleName = moduleName;
            TypeName = typeName;
        }

        public string ModuleName { get; private set; }

        public string TypeName { get; private set; }
    }
}
