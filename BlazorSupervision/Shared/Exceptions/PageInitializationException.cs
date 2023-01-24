// Changelogs Date  | Author                | Description
// 2022-05-16       | Anthony Coudène(ACE)  | Creation - Make code more resilient step 1

using BlazorSupervision.Shared.Exceptions.Base;
using System.Runtime.Serialization;

namespace BlazorSupervision.Shared.Exceptions
{
  [Serializable]
  public class PageInitializationException : LoggedExceptionBase
  {
    public PageInitializationException()
    {
    }

    public PageInitializationException(LoggedExceptionBase ex) 
      : this(ex.Log)
    {

    }

    public PageInitializationException(LogDTO log) 
      : base(log)
    {
    }

    public PageInitializationException(string message) 
      : base(message)
    {
    }

    public PageInitializationException(string message, Exception innerException) 
      : base(message, innerException)
    {
    }

    protected PageInitializationException(SerializationInfo info, StreamingContext context) 
      : base(info, context)
    {
    }
  }
}
