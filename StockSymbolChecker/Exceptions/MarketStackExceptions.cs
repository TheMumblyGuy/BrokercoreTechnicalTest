using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StockSymbolChecker.Exceptions
{
    // Custom exceptions for different API errors
    public class MarketstackException : Exception
    {
        public MarketstackException(string message) : base(message)
        {
        }
    }

    public class UnauthorizedException : MarketstackException
    {
        public UnauthorizedException(string message) : base(message)
        {
        }
    }

    public class HttpsAccessRestrictedException : MarketstackException
    {
        public HttpsAccessRestrictedException(string message) : base(message)
        {
        }
    }

    public class FunctionAccessRestrictedException : MarketstackException
    {
        public FunctionAccessRestrictedException(string message) : base(message)
        {
        }
    }

    public class InvalidApiFunctionException : MarketstackException
    {
        public InvalidApiFunctionException(string message) : base(message)
        {
        }
    }

    public class ResourceNotFoundException : MarketstackException
    {
        public ResourceNotFoundException(string message) : base(message)
        {
        }
    }

    public class TooManyRequestsException : MarketstackException
    {
        public TooManyRequestsException(string message) : base(message)
        {
        }
    }

    public class RateLimitReachedException : MarketstackException
    {
        public RateLimitReachedException(string message) : base(message)
        {
        }
    }

    public class InternalErrorException : MarketstackException
    {
        public InternalErrorException(string message) : base(message)
        {
        }
    }
}