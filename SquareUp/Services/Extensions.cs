using System.Net;
using System.Net.Http.Json;
using SquareUp.Services.Transactions;
using SquareUp.Shared.Types;

namespace SquareUp.Services;


public static class Extension
{
    public static async Task<ServiceResponse<T>> ServiceCall<T>(Func<Task<HttpResponseMessage>> serviceCall)
    {
        try
        {
            return await serviceCall().ServiceResponse<T>();
        }
        catch (Exception e)
        {
            return new ServiceResponse<T>(message: $"Error: {e.Message}");
        }
    }

    public static async Task<HttpResponseMessage> PostAsJsonAsync<T>(this HttpClient http, string uri, T value, bool noException)
    {
        if (!noException) return await http.PostAsJsonAsync(uri, value);

        try
        {
            return await http.PostAsJsonAsync(uri, value);
        }
        catch (Exception e)
        {
            return new HttpResponseMessage { ReasonPhrase = e.Message, StatusCode = HttpStatusCode.ServiceUnavailable };
        }
    }

    public static async Task<HttpResponseMessage> PutAsJsonAsync<T>(this HttpClient http, string uri, T value, bool noException)
    {
        if (!noException) return await http.PutAsJsonAsync(uri, value);

        try
        {
            return await http.PutAsJsonAsync(uri, value);
        }
        catch (Exception e)
        {
            return new HttpResponseMessage { ReasonPhrase = e.Message, StatusCode = HttpStatusCode.ServiceUnavailable };
        }
    }

    public static async Task<HttpResponseMessage> GetAsync<T>(this HttpClient http, string uri, bool noException)
    {
        if (!noException) return await http.GetAsync(uri);

        try
        {
            return await http.GetAsync(uri);
        }
        catch (Exception e)
        {
            return new HttpResponseMessage { ReasonPhrase = e.Message, StatusCode = HttpStatusCode.ServiceUnavailable };
        }
    }

    public static async Task<HttpResponseMessage> DeleteAsync(this HttpClient http, string uri, bool noException)
    {
        if (!noException) return await http.DeleteAsync(uri);

        try
        {
            return await http.DeleteAsync(uri);
        }
        catch (Exception e)
        {
            return new HttpResponseMessage { ReasonPhrase = e.Message, StatusCode = HttpStatusCode.ServiceUnavailable };
        }
    }
}
