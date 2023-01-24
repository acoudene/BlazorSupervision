// Changelogs Date  | Author                | Description
// 2022-05-16       | Anthony Coudène(ACE)  | Creation - Make code more resilient step 1

using System.Runtime.Serialization;

namespace BlazorSupervision.Shared.Exceptions.Base
{
  [Serializable]
  public abstract class LoggedExceptionBase : Exception, ILoggedException
  {
    public LogDTO Log { get; }

    protected LoggedExceptionBase()
    {
      Log = new LogDTO()
      {
        Exception = this
      };
    }

    protected LoggedExceptionBase(LogDTO log)
    {
      Log = log;
    }

    protected LoggedExceptionBase(string message) : base(message)
    {
      Log = new LogDTO()
      {
        Message = message,
        Exception = this
      };
    }

    protected LoggedExceptionBase(string message, Exception innerException) : base(message, innerException)
    {
      Log = new LogDTO()
      {
        Message = message,
        Exception = this,
        InnerCategoryName = innerException?.GetType().Name
      };
    }

    protected LoggedExceptionBase(SerializationInfo info, StreamingContext context) : base(info, context)     
    {
      Log = new LogDTO()
      {
        Exception = this
      };
    }
  }
}
