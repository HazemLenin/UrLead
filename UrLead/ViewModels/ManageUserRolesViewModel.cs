﻿using UrLead.ViewModels;

namespace UrLead.ViewModels
{
    public class ManageUserRolesViewModel
    {
        public string UserId { get; set; }
        public IList<UserRolesViewModel> UserRoles { get; set; }
    }
}
