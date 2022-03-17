using System;

namespace Mhd.Framework.Core
{

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class EnumMemberInfoAttribute : Attribute
    {
        public string Name { get; }

        public string Description { get; }

        private Guid _guid;
        public Guid Guid
        {
            get
            {
                if (_guid == Guid.Empty)
                {
                    if (!string.IsNullOrEmpty(_guidString))
                    {
                        _guid = Guid.Parse(_guidString);
                    }
                }

                return _guid;
            }
        }
        public string Code { get; }

        private Guid? _parentGuid;
        public Guid? ParentGuid
        {
            get
            {
                if (!_parentGuid.HasValue)
                {
                    if (!string.IsNullOrEmpty(_parentGuidString))
                    {
                        _parentGuid = Guid.Parse(_parentGuidString);
                    }
                }

                return _parentGuid;
            }
        }

        private readonly string _guidString;
        private readonly string _parentGuidString;

        public int SortOrder { get; set; }

        public bool IsDefault { get; set; }

        public EnumMemberInfoAttribute(string name, string description, string guid)
        {
            Name = name;
            Description = description;
            _guidString = guid;
        }

        public EnumMemberInfoAttribute(string name, string description, string guid, string code)
            : this(name, description, guid)
        {
            Code = code;
        }

        public EnumMemberInfoAttribute(string name, string description, string guid, string code, string parentGuid)
            : this(name, description, guid, code)
        {
            _parentGuidString = parentGuid;
        }

        public EnumMemberInfoAttribute(string name, string description, string guid, string code, string parentGuid, int sortOrder, bool IsDefault = false)
            : this(name, description, guid, code, parentGuid)
        {
            SortOrder = sortOrder;
            this.IsDefault = IsDefault;
        }
    }
}
