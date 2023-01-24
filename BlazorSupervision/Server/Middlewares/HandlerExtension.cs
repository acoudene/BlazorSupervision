// Changelogs Date  | Author                | Description
// 2022-05-16       | Anthony Coudène(ACE)  | Creation - Make code more resilient step 1

namespace BlazorSupervision.Server.Middlewares
{
  public static class HandlerExtension
  {
    public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder builder)
    {
      return builder.UseMiddleware<ExceptionHandlerMiddleware>();
    }
  }
}
