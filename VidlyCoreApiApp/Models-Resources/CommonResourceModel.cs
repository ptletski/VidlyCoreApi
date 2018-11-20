using System;
using Microsoft.EntityFrameworkCore;
using VidlyCoreApp.Models;

namespace VidlyCoreApiApp.ResourceModels
{
    public class CommonResourceModel : IDisposable
    {
        protected VidlyDbContext _dbContext;

        public CommonResourceModel()
        {
            InitializeDbContext();
        }

        private void InitializeDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<VidlyDbContext>();
            optionsBuilder.UseSqlite("Data Source=vidly.db");

            _dbContext = new VidlyDbContext(optionsBuilder.Options);
        }


        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
