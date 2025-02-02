﻿using Model;
using Service;
using System.Collections.ObjectModel;

namespace Controller
{
    public class UserController
    {
        private readonly UserService userService;
        public User CurentLoggedUser { get; set; }

        public UserController(UserService _service) { userService = _service; }

        public bool Create(User user)
        {
            return userService.Create(user);
        }

        public ObservableCollection<User> FindAll()
        {
            return userService.FindAll();
        }

        public User FindByUsername(string username)
        {
            return userService.FindByUsername(username);
        }

        public bool DeleteByUsername(string username)
        {
            return userService.DeleteByUsername(username);
        }

        public bool UpdateByUsername(string username, User user)
        {
            return userService.UpdateByUsername(username, user);
        }

        public User SendDate(string username, string password)
        {
            return CurentLoggedUser = userService.CheckCredentials(username, password);
        }
    }
}
