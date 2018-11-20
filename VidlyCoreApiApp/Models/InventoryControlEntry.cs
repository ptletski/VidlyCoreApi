using System;
using System.ComponentModel.DataAnnotations;

namespace VidlyCoreApp.Models
{
    public class InventoryControlEntry
    {
        public InventoryControlEntry()
        {
        }

        public int MovieId { get; set; }

        [ContentProviderTypeValidation]
        public int ContentProviderId { get; set; }

        public short PermittedUsageCount { get; set; }

        [Key]
        public int InventoryControlId { get; set; }
    }
}

/*
CREATE TABLE IF NOT EXISTS "InventoryControl" (
    "MovieId" INTEGER NOT NULL,
    "ContentProviderId" INTEGER NOT NULL,
    "PermittedUsageCount" INTEGER NOT NULL,
    "InventoryControlId" INTEGER NOT NULL CONSTRAINT "PK_InventoryControl" PRIMARY KEY AUTOINCREMENT
);
*/