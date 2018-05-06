using System;
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
    }
}
