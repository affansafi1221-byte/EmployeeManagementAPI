using Microsoft.EntityFrameworkCore;
using MyFirstApi.IService;
using MyFirstAPI.Data;
using MyFirstAPI.Dto;
using MyFirstAPI.IServices;
using System.Linq.Expressions;

namespace MyFirstAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        public AuthService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Tuple<int, string>> LoginUser(UserDto dto)
        {
            try
            {

                var existingUser = await _context.AcountUsers.FirstOrDefaultAsync(x => x.Email == dto.Email);
                if (existingUser == null)
                {
                    return new Tuple<int, string>(0, "This User Not Exist, Please Login");
                }
                if (existingUser.Password != dto.Password)
                {
                    return new Tuple<int, string>(1, "Incorrect Password");



                }
                return new Tuple<int, string>(2, "Login Sucessfull");
            }
            catch (Exception)
            {
                return new Tuple<int, string>(3, " Something went wrong");
            }



        }
    }
}