using FriendStorage.Model;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace FriendStorage.DataAccess.Test
{
    public class FileDataServiceTestShould
    {
        private readonly FileDataService _dataService;

        public FileDataServiceTestShould()
        {
            _dataService = new FileDataService();
        }

        [Theory]
        [MemberData(nameof(GetFriends))]
        public void GetFriendById(int id, Friend expected)
        {
            // Act
            var actual = _dataService.GetFriendById(id);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(int.MaxValue)]
        public void GetExceptionWhenDisignatedFriendNotExist(int id)
        {
            // Assert
            Assert.Throws<InvalidOperationException>(
                () => _dataService.GetFriendById(id));
        }

        [Fact]
        public void SaveNotExistFriend()
        {
            // Arrange
            var friend = new Friend
            {
                FirstName = "David",
                LastName = "Stevens",
                Birthday = DateTime.Now,
                IsDeveloper = true,
            };

            // Act
            _dataService.SaveFriend(friend);
            var savedFriend = _dataService.GetFriendById(friend.Id);

            // Assert
            Assert.Equal(friend, savedFriend);
        }

        [Theory]
        [InlineData("Friends.json")]
        public void DeleteStorageFile(string storageFile)
        {
            // Arrange
            var friend = new Friend
            {
                FirstName = "David",
                LastName = "Stevens",
                Birthday = DateTime.Now,
                IsDeveloper = true,
            };
            _dataService.SaveFriend(friend);

            // Act
            _dataService.DeleteStorageFile();

            // Assert
            Assert.False(File.Exists(storageFile));
        }

        public static IEnumerable<object[]> GetFriends()
        {
            return new List<object[]>
            {
                new object[]
                {
                    1,
                    new Friend
                    {
                        Id =1,
                        FirstName = "Thomas",
                        LastName ="Huber",
                        Birthday = new DateTime(1980,10,28),
                        IsDeveloper = true
                    }
                },
                new object[]
                {
                    4,
                    new Friend
                    {
                        Id = 4,
                        FirstName = "Sara",
                        LastName = "Huber",
                        Birthday = new DateTime(2013,02,25)
                    },
                },
                new object[]
                {
                    8,
                    new Friend
                    {
                        Id = 8,
                        FirstName = "Erkan",
                        LastName = "Egin",
                        Birthday = new DateTime(1983,05,23)
                    },
                },
            };
        }
    }
}
