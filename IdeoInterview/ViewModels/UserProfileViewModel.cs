using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IdeoInterview.Models;

namespace IdeoInterview.ViewModels
{
    public class UserProfileViewModel
    {
        public string Role { get; set; }
        public string[] RoleForDropDown => new string[] { "User", "Admin" };
        public string Id { get; set; }

        public UserProfileViewModel()
        {

        }
        public UserProfileViewModel(UserProfile userProfile)
        {
            this.Role = userProfile.Role;
            this.Id = userProfile.Id;
        }
    }
}