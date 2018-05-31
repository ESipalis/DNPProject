using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using WebApplication.Data.Entities;

namespace WebApplication.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser, IUser
    {
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public bool IsManager { get; set; }
        public List<Ticket> Tickets { get; set; }

    }
}