namespace JoshuaWood_ST10296167_PROG7311_POE.Services.Login
{
    public interface ILoginService
    {
        Task<bool> LoginUserAsync(Models.Login login);

        Task LogoutAsync();
    }
}
//--------------------------------------------------------X END OF FILE X-------------------------------------------------------------------//