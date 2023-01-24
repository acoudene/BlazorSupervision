// Changelogs Date  | Author                | Description
// 2022-05-16       | Anthony Coudène(ACE)  | Creation - Make code more resilient step 1

namespace BlazorSupervision.Shared.Exceptions.Base
{
    public interface ILoggedException
    {
        LogDTO Log { get; }
    }
}