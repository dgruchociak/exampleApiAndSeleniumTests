using RestSharp;

namespace exampleFramework.Extensions
{
    public static class RestResponseExtensions
    {
        public static void EnsureStatusSuccess(this RestResponse response)
        {
            if (!response.IsSuccessful)
            {
                throw new InvalidOperationException($"invalid response code: {response.StatusCode}");
            }
        }
    }
}
