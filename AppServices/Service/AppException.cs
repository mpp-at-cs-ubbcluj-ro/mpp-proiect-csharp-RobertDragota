using System;

namespace AppServices.Service;

public class AppException : Exception
{
    // Default constructor
    public AppException() : base()
    {
    }

    // Constructor that accepts a message only
    public AppException(string message) : base(message)
    {
    }

    // Constructor that accepts a message and an inner exception
    public AppException(string message, Exception cause) : base(message, cause)
    {
    }
}