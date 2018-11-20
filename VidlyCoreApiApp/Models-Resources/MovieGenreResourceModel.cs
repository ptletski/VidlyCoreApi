using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using VidlyCoreApp.Models;

namespace VidlyCoreApiApp.ResourceModels
{
    public class MovieGenreResourceModel : CommonResourceModel
    {
        public MovieGenreResourceModel() : base()
        {
        }

        public List<MovieGenre> MovieGenres
        {
            get
            {
                try
                {
                    var movieGenres = _dbContext.MovieGenres;

                    if (movieGenres == null)
                    {
                        return null;
                    }

                    if (movieGenres.Any() == false)
                    {
                        return null;
                    }

                    var resultList = movieGenres.ToList();

                    return resultList;
                }
                catch (Exception exception)
                {
                    Debug.Assert(false, "Failure Gathering Movie Genres");
                    Debug.Assert(false, exception.Message);

                    throw new ResourceFindAllException(exception.Message);
                }
            }
        }
    }
}
