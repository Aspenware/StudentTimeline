using System;
using System.Runtime.Serialization;
using Microsoft.ServiceFabric.Services.Client;
using StudentTimeline.Common;

namespace StudentTimeline.CourseModel
{
    [DataContract]
    public class CourseId : IFormattable, IComparable, IComparable<CourseId>, IEquatable<CourseId>
    {
        [DataMember]
        private Guid id;

        public CourseId()
        {
            this.id = Guid.NewGuid();
        }
        public CourseId(string Id)
        {
            Guid output = new Guid();

            if (Guid.TryParse(Id, out output))
                this.id = output;
            else
                this.id = Guid.Empty;
        }

        public int CompareTo(object obj)
        {
            return this.id.CompareTo(((CourseId)obj).id);
        }

        public int CompareTo(CourseId other)
        {
            return this.id.CompareTo(other.id);
        }

        public bool Equals(CourseId other)
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

        public static bool operator ==(CourseId item1, CourseId item2)
        {
            return item1.Equals(item2);
        }

        public static bool operator !=(CourseId item1, CourseId item2)
        {
            return !item1.Equals(item2);
        }

        public override bool Equals(object obj)
        {
            return (obj is CourseId) ? this.id.Equals(((CourseId)obj).id) : false;
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
