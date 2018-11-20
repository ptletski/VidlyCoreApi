using System;
using System.ComponentModel.DataAnnotations;
using VidlyCoreApp.ViewModels;

namespace VidlyCoreApp.Models
{
    public class Movie
    {
        public Movie()
        {
        }

        [Required]
        public string Title { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        [MpaRatingTypeValidation]
        public byte MpaRatingId { get; set; }

        [Required]
        [MovieGenreTypeValidation]
        public byte MovieGenreId { get; set; }

        [Required]
        public DateTime DateAdded { get; set; }

        public int ActiveUseCount { get; set; }

        [Required]
        public int InventoryControlId { get; set; }

        [Key]
        public int MovieId { get; set; }
    }
}

/*
CREATE TABLE IF NOT EXISTS "Movies" (
    "Title" TEXT NULL,
    "Year" INTEGER NOT NULL,
    "MpaRatingId" INTEGER NOT NULL,
    "MovieGenreId" INTEGER NOT NULL,
    "DateAdded" TEXT NOT NULL,
    "ActiveUseCount" INTEGER NOT NULL,
    "InventoryControlId" INTEGER NOT NULL,
    "MovieId" INTEGER NOT NULL CONSTRAINT "PK_Movies" PRIMARY KEY AUTOINCREMENT
);
*/