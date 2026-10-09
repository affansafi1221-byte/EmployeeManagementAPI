
using MyFirstAPI.Dto;

namespace MyFirstApi.IService
{
    public interface IAuthService
    {
        Task<Tuple<int, TokenDto>> LoginUser(UserDto dto);
        Task<Tuple<int, string>> RegisterUser(UserDto dto);

        Task<string> GenerateQrCode(string email);
        Task<string> VerifyOTP(string email, string otp);
    }
}