using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Data.Entities
{
    public class Movie
    {

        public Movie() {}

        public Movie(string title, DateTime releaseDate, TimeSpan duration)
        {
            Title = title;
            ReleaseDate = releaseDate;
            Duration = duration;
        }

        public int Id { get; set; }

        [StringLength(100, MinimumLength = 1)]
        [Required]
        public string Title { get; set; }

        [DisplayName("Release date")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime ReleaseDate { get; set; }

        [DataType(DataType.Duration)]
        [Required]
        public TimeSpan Duration { get; set; }
    }
}