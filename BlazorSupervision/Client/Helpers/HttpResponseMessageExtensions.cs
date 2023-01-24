// Changelogs Date  | Author                | Description
// 2022-11-22       | Anthony Coudène (ACE) | Creation

using BlazorSupervision.Shared.Exceptions;
using BlazorSupervision.Shared.Exceptions.Base;
using Newtonsoft.Json;
using System.Net.Mime;

namespace BlazorSupervision.Client
{
  /// <summary>
  /// Extensions
  /// </summary>
  public static class HttpResponseMessageExtensions
  {
    /// <summary>
    /// Ensure that http response code is successful and log if not
    /// </summary>
    /// <param name="response"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="UnauthorizedException"></exception>
    /// <exception cref="ApiLegacyException"></exception>
    /// <exception cref="ResponseException"></exception>
    /// <exception cref="ServiceException"></exception>
    public static async Task<HttpResponseMessage> EnsureAndLogSuccessStatusCodeAsync(this HttpResponseMessage response, CancellationToken cancellationToken)
    {
      if (response.IsSuccessStatusCode)
        return response;

      try
      {
        using var content = response.Content;

        if (content == null)
          return response.EnsureSuccessStatusCode();

        var mediaType = content.Headers.ContentType?.MediaType;

        if (mediaType == null || !mediaType.Equals(MediaTypeNames.Application.Json))
          return response.EnsureSuccessStatusCode();

        var stringContent = await content.ReadAsStringAsync(cancellationToken);

        if (string.IsNullOrWhiteSpace(stringContent))
          return response.EnsureSuccessStatusCode();

        var log = JsonConvert.DeserializeObject<LogDTO>(stringContent);

        if (log == null)
          return response.EnsureSuccessStatusCode();

        throw new ResponseException(log);
      }
      catch (LoggedExceptionBase)
      {
        throw;
      }
      catch (Exception ex)
      {
        throw new ServerException(ex.Message, ex);
      }
    }
  }
}
