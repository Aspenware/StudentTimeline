
using System;
using System.Runtime.Serialization;
using Microsoft.ServiceFabric.Services.Client;
using StudentTimeline.Common;

namespace StudentTimeline.UserModel
{
    [DataContract]
    public class UserId : IFormattable, IComparable, IComparable<UserId>, IEquatable<UserId>
    {
        [DataMember]
        private Guid id;

        public UserId()
        {
            this.id = Guid.NewGuid();
        }

        public int CompareTo(object obj)
        {
            return this.id.CompareTo(((UserId)obj).id);
        }

        public int CompareTo(UserId other)
        {
            return this.id.CompareTo(other.id);
        }

        public bool Equals(UserId other)
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

        public static bool operator ==(UserId item1, UserId item2)
        {
            return item1.Equals(item2);
        }

        public static bool operator !=(UserId item1, UserId item2)
        {
            return !item1.Equals(item2);
        }

        public override bool Equals(object obj)
        {
            return (obj is UserId) ? this.id.Equals(((UserId)obj).id) : false;
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