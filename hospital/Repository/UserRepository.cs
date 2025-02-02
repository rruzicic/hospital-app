﻿using hospital.FileHandler;
using Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Repository
{
    public class UserRepository
    {
        public UserFileHandler userFileHandler;
        public ObservableCollection<User> users;

        public UserRepository()
        {
            userFileHandler = new UserFileHandler();


            List<User> deserializedList = userFileHandler.Read();
            if (deserializedList != null)
            {
                users = new ObservableCollection<User>(userFileHandler.Read());
            }
            else
            {
                users = new ObservableCollection<User>();
            }

        }


        public bool Create(User user)
        {
            users.Add(user);
            userFileHandler.Write(users.ToList());
            return true;
        }

        public ObservableCollection<User> FindAll()
        {
            return users;
        }

        public User FindByUsername(string username)
        {
            foreach (User p in users)
            {
                if (p.Username.Equals(username))
                {
                    return p;
                }
            }
            return null;
        }


        public bool DeleteByUsername(string username)
        {
            bool reVal = users.Remove(FindByUsername(username));
            userFileHandler.Write(users.ToList());
            return reVal;
        }

        public bool UpdateByUsername(string username, User user)
        {
            User u = FindByUsername(username);
            u.Username = user.Username;
            u.Role = user.Role;
            u.IsBlocked = user.IsBlocked;
            return true;
        }


        public ObservableCollection<User> Users
        {
            get
            {
                if (users == null)
                {
                    users = new ObservableCollection<User>();
                }

                return users;
            }
            set
            {
                RemoveAllUsers();
                if (value != null)
                {
                    foreach (User oUser in value)
                    {
                        AddUser(oUser);
                    }
                }
            }
        }


        public void AddUser(User newUser)
        {
            if (newUser == null)
            {
                return;
            }

            if (users == null)
            {
                users = new ObservableCollection<User>();
            }

            if (!users.Contains(newUser))
            {
                users.Add(newUser);
            }
        }


        public void RemoveUser(Patient oldUser)
        {
            if (oldUser == null)
            {
                return;
            }

            if (users != null)
            {
                if (users.Contains(oldUser))
                {
                    users.Remove(oldUser);
                }
            }
        }


        public void RemoveAllUsers()
        {
            if (users != null)
            {
                users.Clear();
            }
        }

    }
}
