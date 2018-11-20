using System;

namespace VidlyCoreApp.Models
{
    public class MovieAndInventory
    {
        public MovieAndInventory()
        {
        }

        public Movie Movie { get; set; }

        public InventoryControlEntry InventoryControlEntry { get; set; }
    }
}
