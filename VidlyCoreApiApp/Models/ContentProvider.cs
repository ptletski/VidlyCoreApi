using System;
using System.ComponentModel.DataAnnotations;

namespace VidlyCoreApp.Models
{
    public class ContentProvider
    {
        public static readonly int First = 1;
        public static readonly int SonyMotionPictures = 1;
        public static readonly int UnitedArtists = 2;
        public static readonly int TwentiethCenturyFox = 3;
        public static readonly int ColumbiaPictures = 4;
        public static readonly int DisneyMotionPictures = 5;
        public static readonly int MetroGoldwinMayer = 6;
        public static readonly int Last = 6;

        public ContentProvider()
        {
        }

        public string ContentProviderName { get; set; }
        public string ContractReference { get; set; }

        [Key]
        public int ContentProviderId { get; set; }
    }
}
