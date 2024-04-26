namespace Lab3.Utility.Validation;

using System;

public class ValidException : Exception
{
    public ValidException()
    {
    }

    public ValidException(string message) : base(message)
    {
    }

    public ValidException(string message, Exception cause) : base(message, cause)
    {
    }

    // In C#, there's no direct equivalent of the Java constructor that accepts enableSuppression and writableStackTrace.
    // If you need to control these aspects in C#, you would typically do it by overriding properties or methods in your custom exception class.
}
