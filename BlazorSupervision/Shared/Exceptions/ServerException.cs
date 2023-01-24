// Changelogs Date  | Author                | Description
// 2022-05-16       | Anthony Coudène(ACE)  | Creation - Make code more resilient step 1

using BlazorSupervision.Shared.Exceptions.Base;
using System.Net;
using System.Runtime.Serialization;

namespace BlazorSupervision.Shared.Exceptions
{
  [Serializable]
  public class ServerException : LoggedExceptionBase
  {
    public HttpStatusCode StatusCode { get; protected set; } 

    public ServerException()
    {
      StatusCode = HttpStatusCode.InternalServerError;
    }

    public ServerException(LoggedExceptionBase ex) 
      : this(ex.Log)
    {
      StatusCode = HttpStatusCode.InternalServerError;
    }

    public ServerException(LogDTO log) 
      : base(log)
    {
      StatusCode = HttpStatusCode.InternalServerError;
    }

    public ServerException(string message) 
      : base(message)
    {
      StatusCode = HttpStatusCode.InternalServerError;
    }

    public ServerException(string message, HttpStatusCode statusCode) 
      : base(message)
    {
      StatusCode = statusCode;
    }

    public ServerException(string message, Exception innerException) 
      : base(message, innerException)
    {
      StatusCode = HttpStatusCode.InternalServerError;
    }

    protected ServerException(SerializationInfo info, StreamingContext context) 
      : base(info, context)
    {
      StatusCode = HttpStatusCode.InternalServerError;
    }
  }
}
