using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Data.Entities
{
    public interface IUser
    {
        [DisplayName("Full name")]
        [StringLength(250, MinimumLength = 5)]
        [Required]
        string FullName { get; set; }

        [DisplayName("Birth date")]
        [DataType(DataType.Date)]
        [Required]
        DateTime BirthDate { get; set; }

        List<Ticket> Tickets { get; set; }
    }
}