using System;

namespace MonetIntro.Exceptions
{
    public class FileUploadException : Exception
    {
        public FileUploadException(string message) : base(message)
        {
        }
    }
}
