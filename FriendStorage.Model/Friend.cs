using System;
using System.Collections.Generic;

namespace FriendStorage.Model
{
    public class Friend : IEquatable<Friend>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? Birthday { get; set; }
        public bool IsDeveloper { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Friend);
        }

        public bool Equals(Friend other)
        {
            return other != null &&
                   Id == other.Id &&
                   FirstName == other.FirstName &&
                   LastName == other.LastName &&
                   EqualityComparer<DateTime?>.Default.Equals(Birthday, other.Birthday) &&
                   IsDeveloper == other.IsDeveloper;
        }

        public override int GetHashCode()
        {
            var hashCode = -275966267;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(FirstName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LastName);
            hashCode = hashCode * -1521134295 + EqualityComparer<DateTime?>.Default.GetHashCode(Birthday);
            hashCode = hashCode * -1521134295 + IsDeveloper.GetHashCode();
            return hashCode;
        }
    }
}
