
using System;
using System.Runtime.Serialization;
using Microsoft.ServiceFabric.Services.Client;
using StudentTimeline.Common;

namespace StudentTimeline.TaskModel
{

    [DataContract]
    public class TaskId : IFormattable, IComparable, IComparable<TaskId>, IEquatable<TaskId>
    {
        [DataMember]
        private Guid id;

        public TaskId()
        {
            this.id = Guid.NewGuid();
        }

        public int CompareTo(object obj)
        {
            return this.id.CompareTo(((TaskId)obj).id);
        }

        public int CompareTo(TaskId other)
        {
            return this.id.CompareTo(other.id);
        }

        public bool Equals(TaskId other)
        {
            return this.id.Equals(other.id);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return this.id.ToString(format, formatProvider);
        }

        public ServicePartitionKey GetPartitionKey()
        {
            return new ServicePartitionKey(HashUtil.getLongHashCode(this.id.ToString()));
        }

        public static bool operator ==(TaskId item1, TaskId item2)
        {
            return item1.Equals(item2);
        }

        public static bool operator !=(TaskId item1, TaskId item2)
        {
            return !item1.Equals(item2);
        }

        public override bool Equals(object obj)
        {
            return (obj is TaskId) ? this.id.Equals(((TaskId)obj).id) : false;
        }

        public override int GetHashCode()
        {
            return this.id.GetHashCode();
        }

        public override string ToString()
        {
            return this.id.ToString();
        }

        public string ToString(string format)
        {
            return this.id.ToString(format);
        }
    }
}

