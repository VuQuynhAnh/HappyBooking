namespace HappyBookingClient.Service.IService;

public interface IBaseApiService
{
    Task<string> GetTokenFromLocalStorageAsync();
}
