using System;

namespace VidlyCoreApiApp.ResourceModels
{
    public static class VidlyApiServiceFailure
    {
        static public readonly int ServiceFailure = 500;
        static public readonly int ServiceNotImplemented = 501;
    }

    public class ResourceFindAllException : Exception
    {
        public ResourceFindAllException(string message) : base(message)
        {
        }
    }

    public class ResourceFindException : Exception
    {
        public ResourceFindException(string message) : base(message)
        {
        }
    }

    public class ResourceAddException : Exception
    {
        public ResourceAddException(string message) : base(message)
        {
        }
    }

    public class ResourceUpdateException : Exception
    {
        public ResourceUpdateException(string message) : base(message)
        {
        }
    }

    public class ResourceDeleteException : Exception
    {
        public ResourceDeleteException(string message) : base(message)
        {
        }
    }
}
