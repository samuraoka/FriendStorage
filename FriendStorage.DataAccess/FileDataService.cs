﻿using FriendStorage.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FriendStorage.DataAccess
{
    public class FileDataService : IDataService
    {
        private const string StorageFile = "Friends.json";

        public Friend GetFriendById(int friendId)
        {
            var friends = ReadFromFile();
            return friends.Single(f => f.Id == friendId);
        }

        public void SaveFriend(Friend friend)
        {
            throw new NotImplementedException();
        }

        public void DeleteFriend(int friendId)
        {
            throw new NotImplementedException();
        }

        // TODO

        public IEnumerable<Friend> GetAllFriends()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        // TODO

        private List<Friend> ReadFromFile()
        {
            if (File.Exists(StorageFile) == false)
            {
                return new List<Friend>
                {
                    new Friend
                    {
                        Id = 1,
                        FirstName = "Thomas",
                        LastName = "Huber",
                        Birthday = new DateTime(1980,10,28),
                        IsDeveloper = true
                    },
                    new Friend
                    {
                        Id = 2,
                        FirstName = "Julia",
                        LastName = "Huber",
                        Birthday = new DateTime(1982,10,10)
                    },
                    new Friend
                    {
                        Id = 3,
                        FirstName = "Anna",
                        LastName = "Huber",
                        Birthday = new DateTime(2011,05,13)
                    },
                    new Friend
                    {
                        Id = 4,
                        FirstName = "Sara",
                        LastName = "Huber",
                        Birthday = new DateTime(2013,02,25)
                    },
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
                        Id = 6,
                        FirstName = "Urs",
                        LastName = "Meier",
                        Birthday = new DateTime(1970,03,5),
                        IsDeveloper = true
                    },
                    new Friend
                    {
                        Id = 7,
                        FirstName = "Chrissi",
                        LastName = "Heuberger",
                        Birthday = new DateTime(1987,07,16)
                    },
                    new Friend
                    {
                        Id = 8,
                        FirstName = "Erkan",
                        LastName = "Egin",
                        Birthday = new DateTime(1983,05,23)
                    },
                };
            }

            string json = File.ReadAllText(StorageFile);
            // Newtonsoft.Json
            // https://www.nuget.org/packages/Newtonsoft.Json/
            // Install-Package -Id Newtonsoft.Json -Project FriendStorage.DataAccess
            return JsonConvert.DeserializeObject<List<Friend>>(json);
        }
    }
}