using System;
using System.Collections.Generic;
using Xunit;

namespace FriendStorage.Model.Test
{
    public class FriendTestShould
    {
        private readonly Type _type;

        public FriendTestShould()
        {
            // Arrange
            _type = typeof(Friend);
        }

        [Theory]
        [InlineData("Id", typeof(int))]
        [InlineData("FirstName", typeof(string))]
        [InlineData("LastName", typeof(string))]
        [InlineData("Birthday", typeof(DateTime?))]
        [InlineData("IsDeveloper", typeof(bool))]
        public void AccessReadWriteProperty(string propName, Type propType)
        {
            // Act
            var propInfo = _type.GetProperty(propName);

            // Assert
            Assert.NotNull(propInfo);
            Assert.Equal(propType, propInfo.PropertyType);
            Assert.True(propInfo.CanRead);
            Assert.True(propInfo.CanWrite);
        }

        [Theory]
        [MemberData(nameof(GetEqualityCheckData))]
        public void VarifyEquality(Friend a, Friend b, bool expected)
        {
            Assert.True(expected == a.Equals(b));
        }

        [Theory]
        [MemberData(nameof(GetEqualityCheckData))]
        public void VarifyHashCodeEquality(Friend a, Friend b, bool expected)
        {
            Assert.True(expected == (a.GetHashCode() == b.GetHashCode()));
        }

        public static IEnumerable<object[]> GetEqualityCheckData()
        {
            return new List<object[]>
            {
                new object[]
                {
                    new Friend
                    {
                        Id = 3,
                        FirstName = "Anna",
                        LastName = "Huber",
                        Birthday = new DateTime(2011,05,13)
                    },
                    new Friend
                    {
                        Id = 3,
                        FirstName = "Anna",
                        LastName = "Huber",
                        Birthday = new DateTime(2011,05,13)
                    },
                    true,
                },
                new object[]
                {
                    new Friend
                    {
                        Id = 5,
                        FirstName = "Andreas",
                        LastName = "Böhler",
                        Birthday = new DateTime(1981,01,10),
                        IsDeveloper = true
                    },
                    new Friend
                    {
                        Id = 7,
                        FirstName = "Chrissi",
                        LastName = "Heuberger",
                        Birthday = new DateTime(1987,07,16)
                    },
                    false
                },
            };
        }
    }
}
