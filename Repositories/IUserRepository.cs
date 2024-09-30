using core_admin.Models.DTO;

namespace core_admin.Repositories
{
    public interface IUserRepository
    {
        Task<string> RegisterUserAsync(DTORegister model);
        Task<DTOLoginResponse> LoginUserAsync(DTOLogin model);
        string GetTestMessage();
    }
}
