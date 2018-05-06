using FriendStorage.Model;
using System;
using System.Collections.Generic;
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
