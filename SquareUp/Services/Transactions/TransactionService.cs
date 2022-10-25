using System.Net.Http.Json;
using SquareUp.Model;
using SquareUp.Shared.Requests;
using SquareUp.Shared.Types;
using static SquareUp.Shared.Endpoints.Transactions;

namespace SquareUp.Services.Transactions;

public class TransactionService : ITransactionService
{
    private readonly HttpClient _http;

    public TransactionService(HttpClient http) => _http = http;
    public async Task<ServiceResponse<ObservableTransaction>> Create(ObservableTransaction transaction, int groupId)
    {
        transaction.ParticipantId = transaction.Participant.Id;
        transaction.SecondaryParticipantId = transaction.SecondaryParticipant.Id;

        var result = await _http
            .PostAsJsonAsync(PostAddTransactionUri, new TransactionRequest(transaction, groupId))
            .ServiceResponse<ObservableTransaction>();

        return result;
    }

    public async Task<ServiceResponse<ObservableTransaction>> Update(ObservableTransaction transaction)
    {
        transaction.ParticipantId = transaction.Participant.Id;
        transaction.SecondaryParticipantId = transaction.SecondaryParticipant.Id;

        var result = await _http
            .PutAsJsonAsync(PutEditTransactionUri, transaction)
            .ServiceResponse<ObservableTransaction>();

        

        return result;
    }

    public async Task<ServiceResponse<int>> Delete(ObservableTransaction transaction)
    {
        transaction.ParticipantId = transaction.Participant.Id;
        transaction.SecondaryParticipantId = transaction.SecondaryParticipant.Id;

        var result = await _http
           .DeleteAsync(DeleteTransactionUri(transaction.Id))
           .ServiceResponse<int>();

       return result;
    }

}

public static class Extension
{
    public static async Task<ServiceResponse<T>> ServiceResponse<T>(this Task<HttpResponseMessage> task)
    {
        var response = await task;
        return await response.ServiceResponse<T>();
    }

    public static async Task<ServiceResponse<T>> ServiceResponse<T>(this HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
            return new ServiceResponse<T> (message: $"Request failed. {response.ReasonPhrase}");

        try
        {
            var result = await response.Content.ReadFromJsonAsync<ServiceResponse<T>>();
            var str = await response.Content.ReadAsStringAsync();
            return result;
        }
        catch (Exception e)
        {
            return new ServiceResponse<T>(message:$"Parse failed: {e.Message}");
        }
    }
}
