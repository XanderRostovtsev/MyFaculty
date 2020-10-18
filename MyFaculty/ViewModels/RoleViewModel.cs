﻿using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

public class RoleViewModel
{
    public string UserId { get; set; }
    public string UserEmail { get; set; }
    public string UserName { get; set; }
    public List<IdentityRole> AllRoles { get; set; }
    public IList<string> UserRoles { get; set; }
    public RoleViewModel()
    {
        AllRoles = new List<IdentityRole>();
        UserRoles = new List<string>();
    }
}
