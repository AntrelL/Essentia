namespace Essentia.Reflection
{
    public static class Package
    {
        public const string Name = "Essentia";
        public const string TechnicalName = "com.antrel.essentia";
        public const string Path = "Packages/" + TechnicalName;
        public const string PathToDynamicDataFolder = Metadata.AssetRootFolderName + "/" + Name + "Data";
        public const string SystemAddressablesGroupName = "System";

        public static class ModuleName
        {
            public const string Misc = nameof(Misc);
            public const string Disk = nameof(Disk);
            public const string Debug = nameof(Debug);
            public const string Reflection = nameof(Reflection);
            public const string Deployment = nameof(Deployment);
            public const string ObjectControl = nameof(ObjectControl);
            public const string Infrastructure = nameof(Infrastructure);
        }
    }
}
