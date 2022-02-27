using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Countware.Traffic.CrossCC.AuditLog
{
    public class AuditTrailHelper
    {
        public static Audit AuditTrailFactory(DbContext objectContext, EntityEntry entry, Guid _userId)
        {
            var audit = new Audit
            {
                RevisionStamp = DateTime.Now,
                TableName = entry.Entity.GetType().Name,
                UserName = "",
                UserId = _userId
            };

            switch (entry.State)
            {
                case EntityState.Added:
                    {
                        static string GetEntryValues(EntityEntry entry)
                        {
                            var json = new StringBuilder("{");
                            int index = 0;

                            foreach (var member in entry.Members)
                            {
                                if (index > 0)
                                    json.Append(",");

                                json.Append($"\"{member.Metadata.Name}\" : \"{member.CurrentValue}\"");
                                index++;
                            }

                            json.Append("}");
                            return json.ToString();
                        }
                        audit.NewData = GetEntryValues(entry);
                        var current = entry.CurrentValues;
                        audit.RecordId = entry.Metadata.FindPrimaryKey().Properties.Select(p => entry.Property(p.Name).CurrentValue).FirstOrDefault().ToString();
                        audit.Actions = entry.State.ToString();
                        break;
                    }



                case EntityState.Deleted:
                    {
                        static string GetEntryValues(EntityEntry entry)
                        {
                            var json = new StringBuilder("{");
                            var propertyEntry = entry.Properties.Where(u => u.Metadata.Name == "Id").FirstOrDefault();

                            if (propertyEntry != null)
                                json.Append($"\"{propertyEntry.Metadata.Name}\" : \"{ propertyEntry.OriginalValue}\"");

                            json.Append("}");
                            return json.ToString();
                        }
                        audit.OldData = GetEntryValues(entry);
                        audit.Actions = entry.State.ToString();
                        audit.RecordId = entry.Metadata.FindPrimaryKey().Properties.Select(p => entry.Property(p.Name).CurrentValue).FirstOrDefault().ToString();
                        break;
                    }



                default:
                    {
                        static (string Data, string ChangedColumns) GetEntryValues(EntityEntry entry, bool isOrginal)
                        {
                            var changedColumnList = new List<string>();
                            var json = new StringBuilder("{");
                            int index = 0;

                            foreach (var propertyEntry in entry.Properties)
                            {
                                if (propertyEntry.IsModified)
                                {
                                    string oldValue = propertyEntry.OriginalValue != null ? propertyEntry.OriginalValue.ToString() : "";
                                    string newValue = propertyEntry.CurrentValue != null ? propertyEntry.CurrentValue.ToString() : "";

                                    if (oldValue != newValue)
                                    {
                                        changedColumnList.Add(propertyEntry.Metadata.Name);

                                        if (index > 0)
                                            json.Append(",");

                                        json.Append($"\"{propertyEntry.Metadata.Name}\" : \"{(isOrginal ? oldValue : newValue)}\"");
                                        index++;
                                    }
                                }
                            }
                            json.Append("}");
                            return (json.ToString(), string.Join(",", changedColumnList.ToArray()));
                        }
                        audit.OldData = GetEntryValues(entry, true).Data;
                        var newEntry = GetEntryValues(entry, false);
                        audit.NewData = newEntry.Data;
                        audit.Actions = entry.State.ToString();
                        audit.RecordId = entry.Metadata.FindPrimaryKey().Properties.Select(p => entry.Property(p.Name).CurrentValue).FirstOrDefault().ToString();
                        audit.ChangedColumns = newEntry.ChangedColumns;
                    }
                    break;
            }
            return audit;
        }
    }
}
