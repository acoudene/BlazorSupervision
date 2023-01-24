// Changelogs Date  | Author                | Description
// 2022-05-16       | Anthony Coudène(ACE)  | Creation - Make code more resilient step 1

using BlazorSupervision.Shared.Exceptions.Base;
using System.Net;
using System.Runtime.Serialization;

namespace BlazorSupervision.Shared.Exceptions
{
  [Serializable]
  public class ControllerException : ServerException
  {    
    public ControllerException()
    {
    }

    public ControllerException(LoggedExceptionBase ex) 
      : base(ex.Log)
    {

    }

    public ControllerException(LogDTO log) 
      : base(log)
    {
    }

    public ControllerException(string message) 
      : base(message)
    {
    }

    public ControllerException(string message, HttpStatusCode statusCode) 
      : base(message, statusCode)
    {      
    }

    public ControllerException(string message, Exception innerException) 
      : base(message, innerException)
    {
    }

    protected ControllerException(SerializationInfo info, StreamingContext context) 
      : base(info, context)
    {
    }
  }
}
