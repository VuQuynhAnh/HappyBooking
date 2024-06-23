namespace HappyBookingClient.Service;

public interface IBaseApiService
{
    Task<string> GetTokenFromLocalStorageAsync();
}
