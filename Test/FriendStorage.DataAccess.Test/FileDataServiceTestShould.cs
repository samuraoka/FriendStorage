using FriendStorage.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        [Fact]
        public void IncreaseOneFriendWhenAddedNewFriend()
        {
            // Arrange
            var numberBefore = _dataService.GetAllFriends().Count();

            // Act
            _dataService.SaveFriend(new Friend
            {
                FirstName = "David",
                LastName = "Stevens",
                Birthday = DateTime.Now,
                IsDeveloper = true,
            });
            var numberAfter = _dataService.GetAllFriends().Count();

            // Assert
            Assert.Equal(numberBefore + 1, numberAfter);
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

        [Theory]
        [InlineData("Andrea", "Iannone", 1989, 8, 9)]
        public void ModifyExistingFriend(
            string firstName, string lastName,
            int birthYear, int birthMonth, int birthDay)
        {
            // Arrange
            var tmp = new Friend();
            _dataService.SaveFriend(tmp);
            var friend = _dataService.GetFriendById(tmp.Id);

            // Act
            friend.FirstName = firstName;
            friend.LastName = lastName;
            friend.Birthday = new DateTime(birthYear, birthMonth, birthDay);
            friend.IsDeveloper = true;
            _dataService.SaveFriend(friend);
            var updatedFriend = _dataService.GetFriendById(friend.Id);

            // Assert
            Assert.NotSame(friend, updatedFriend);
            Assert.Equal(friend, updatedFriend);
        }

        [Theory]
        [InlineData("Alvaro", "Bautista", 1984, 11, 21)]
        public void NotIncreaseFriendsWhenUpdatingFriend(
            string firstName, string lastName,
            int birthYear, int birthMonth, int birthDay)
        {
            // Arrange
            var tmp = new Friend();
            _dataService.SaveFriend(tmp);
            var friend = _dataService.GetFriendById(tmp.Id);
            var numberBefore = _dataService.GetAllFriends().Count();

            // Act
            friend.FirstName = firstName;
            friend.LastName = lastName;
            friend.Birthday = new DateTime(birthYear, birthMonth, birthDay);
            friend.IsDeveloper = true;
            _dataService.SaveFriend(friend);
            var numberAfter = _dataService.GetAllFriends().Count();

            // Assert
            Assert.Equal(numberBefore, numberAfter);
        }

        [Fact]
        public void GetAllFriends()
        {
            // Act
            var friends = _dataService.GetAllFriends();

            // Assert
            Assert.NotNull(friends);
        }

        [Fact]
        public void DeleteFriend()
        {
            // Arrange
            var tmp = new Friend();
            _dataService.SaveFriend(tmp);

            // Act
            _dataService.DeleteFriend(tmp.Id);

            // Assert
            Assert.Throws<InvalidOperationException>(
                () => _dataService.GetFriendById(tmp.Id));
        }

        [Fact]
        public void DecreaseOneFriendWhenDeleteFriend()
        {
            // Arrange
            var tmp = new Friend();
            _dataService.SaveFriend(tmp);
            var numberBefore = _dataService.GetAllFriends().Count();

            // Act
            _dataService.DeleteFriend(tmp.Id);
            var numberAfter = _dataService.GetAllFriends().Count();

            // Assert
            Assert.Equal(numberBefore - 1, numberAfter);
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
