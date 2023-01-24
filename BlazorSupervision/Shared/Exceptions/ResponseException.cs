// Changelogs Date  | Author                | Description
// 2022-05-16       | Anthony Coudène(ACE)  | Creation - Make code more resilient step 1

using BlazorSupervision.Shared.Exceptions.Base;
using System.Runtime.Serialization;

namespace BlazorSupervision.Shared.Exceptions
{
  [Serializable]
  public class ResponseException : LoggedExceptionBase
  {
    public ResponseException()
    {
    }
    public ResponseException(LogDTO log) : base(log)
    {
    }

    public ResponseException(string message) : base(message)
    {
    }

    public ResponseException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected ResponseException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
  }
}
