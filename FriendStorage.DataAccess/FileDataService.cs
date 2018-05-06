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
            if (friend.Id <= 0)
            {
                InsertFriend(friend);
            }
            else
            {
                UpdateFriend(friend);
            }
        }

        public void DeleteFriend(int friendId)
        {
            throw new NotImplementedException();
        }

        private void UpdateFriend(Friend friend)
        {
            var friends = ReadFromFile();
            var existing = friends.Single(f => f.Id == friend.Id);
            var indexOfExisting = friends.IndexOf(existing);
            friends.Insert(indexOfExisting, friend);
            friends.Remove(existing);
            SaveToFile(friends);
        }

        private void InsertFriend(Friend friend)
        {
            var friends = ReadFromFile();
            var nextFriendId =
                (friends.Count == 0 ? 0 : friends.Max(f => f.Id)) + 1;
            friend.Id = nextFriendId;
            friends.Add(friend);
            SaveToFile(friends);
        }

        public IEnumerable<Friend> GetAllFriends()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void DeleteStorageFile()
        {
            File.Delete(StorageFile);
        }

        private void SaveToFile(IEnumerable<Friend> friends)
        {
            string json =
                JsonConvert.SerializeObject(friends, Formatting.Indented);
            File.WriteAllText(StorageFile, json);
        }

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
