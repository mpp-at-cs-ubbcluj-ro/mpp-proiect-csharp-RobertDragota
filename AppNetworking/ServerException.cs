﻿using System;

public class ServerException : Exception
{
    public ServerException() : base()
    {
    }

    public ServerException(string message) : base(message)
    {
    }

    public ServerException(string message, Exception cause) : base(message, cause)
    {
    }
}