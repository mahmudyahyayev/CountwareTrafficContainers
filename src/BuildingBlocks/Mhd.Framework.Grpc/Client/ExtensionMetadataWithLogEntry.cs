using Grpc.Core;
using System;
using System.Linq;
using static Grpc.Core.Metadata;

namespace Mhd.Framework.Grpc.Client
{
    public class ExtensionMetadataWithLogEntry
    {
        private static readonly Lazy<ExtensionMetadataWithLogEntry> lazy = new(() => new ExtensionMetadataWithLogEntry());
        public static ExtensionMetadataWithLogEntry Current => lazy.Value;
        private readonly Metadata _metaData;
        private ExtensionMetadataWithLogEntry() => _metaData = new Metadata() { new Entry("HasClientsideLog", "true") };

        public void AddEntry(string key, string value)
        {
            if (!_metaData.Any(u => u.Key.Equals(key, StringComparison.OrdinalIgnoreCase)))
                _metaData.Add(new Entry(key, value));
        }
        public Metadata GetMetadata() => _metaData;
    }
}
