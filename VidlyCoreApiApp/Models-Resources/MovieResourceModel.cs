using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using VidlyCoreApp.Models;

namespace VidlyCoreApiApp.ResourceModels
{
    public class MovieResourceModel : CommonResourceModel
    {
        public MovieResourceModel() : base()
        {
        }

        public List<Movie> Movies
        {
            get 
            {
                try
                {
                    var movies = _dbContext.Movies;

                    if (movies == null)
                    {
                        return null;
                    }

                    if (movies.Any() == false)
                    {
                        return null;
                    }

                    var movieList = movies.ToList();

                    return movieList;
                }
                catch (Exception exception)
                {
                    Debug.Assert(false, "Failure Gathering Movies");
                    Debug.Assert(false, exception.Message);

                    throw new ResourceFindAllException(exception.Message);
                }
            }
        }

        public Movie Find(int id)
        {
            Movie movie = null;

            try
            {
                movie = _dbContext.Movies.Single(m => m.MovieId == id);
            }
            catch (Exception exception)
            {
                Debug.Assert(false, "Failure Finding Movie by Id");
                Debug.Assert(false, exception.Message);

                throw new ResourceFindException(exception.Message);
            }

            return movie;
        }

        public int Add(Movie movie, InventoryControlEntry inventory)
        {
            try
            {   // Two transactions. 
                // First: Create movie with 0 InventoryControlId.
                // Second: Create Inventory entry with usage count and content provider.
                // Assumption: No new suppliers. Out of bounds transaction.
                // Business Process: Add ContentProvider if needed, 
                //  then add Movie with "Number of Licenses",
                //  then add InventoryControl
                // ADD TRANSACTION SCOPING
                Movie targetMovie = new Movie
                {
                    Title = movie.Title,
                    Year = movie.Year,
                    MpaRatingId = movie.MpaRatingId,
                    MovieGenreId = movie.MovieGenreId,
                    DateAdded = movie.DateAdded,
                    ActiveUseCount = 0,
                    InventoryControlId = 0
                };

                _dbContext.Movies.Add(targetMovie);
                _dbContext.SaveChanges();

                // Update Inventory
                targetMovie.MovieId = _dbContext.RetrieveLastAutoIncrementKey(VidlyDbContext.MoviesTable);

                InventoryControlEntry inventoryUpdate = new InventoryControlEntry
                {
                    ContentProviderId = inventory.ContentProviderId,
                    PermittedUsageCount = inventory.PermittedUsageCount,
                    MovieId = targetMovie.MovieId
                };

                _dbContext.InventoryControl.Add(inventoryUpdate);
                _dbContext.SaveChanges();

                // Reflect in Movie
                targetMovie.InventoryControlId = _dbContext.RetrieveLastAutoIncrementKey(VidlyDbContext.InventoryControlTable);

                Update(targetMovie);

                return targetMovie.MovieId;
            }
            catch (Exception exception)
            {
                Debug.Assert(false, "Failure Adding Movie");
                Debug.Assert(false, exception.Message);

                throw new ResourceAddException(exception.Message);
            }
        }

        public bool Update(Movie movieUpdate)
        {
            try
            {
                bool isUpdated = false;
                var existingMovie = _dbContext.Movies.Find(movieUpdate.MovieId);

                if (existingMovie != null)
                {
                    _dbContext.Entry(existingMovie).CurrentValues.SetValues(movieUpdate); // !!
                    _dbContext.SaveChanges();

                    isUpdated = true;
                }

                return isUpdated;
            }
            catch (Exception exception)
            {
                Debug.Assert(false, $"Failure Updating Movie {movieUpdate.MovieId}");
                Debug.Assert(false, exception.Message);

                throw new ResourceUpdateException(exception.Message);
            }
        }

        public bool Delete(int movieId)
        {
            try
            {
                bool isDeleted = false;
                var movie = _dbContext.Movies.Find(movieId);
                int inventoryControlId = movie.InventoryControlId;
                var inventoryControl = _dbContext.InventoryControl.Find(inventoryControlId);

                if (movie != null)
                {
                    _dbContext.Movies.Remove(movie);
                    isDeleted = true;
                }

                if (isDeleted)
                {
                    if (inventoryControl != null)
                    {
                        _dbContext.InventoryControl.Remove(inventoryControl);
                        _dbContext.SaveChanges();
                        return isDeleted;
                    }
                    else
                    {
                        throw new ResourceDeleteException("Failure deleting movie resource. No related Inventory Control item.");
                    }
                }
                else
                {
                    throw new ResourceDeleteException("Failure deleting movie resource. No related Movie item.");
                }

            }
            catch (Exception exception)
            {
                Debug.Assert(false, $"Failure Deleting Movie With ID: {movieId}");
                Debug.Assert(false, exception.Message);

                throw new ResourceDeleteException(exception.Message);
            }
        }
    }
}
