
using SalesOrderAPI.Models;

namespace SalesOrderAPI.Repository.Interfaces
{
    public interface IAuthRepository
    {
        public string Login(LoginRequest request);
    }
}
