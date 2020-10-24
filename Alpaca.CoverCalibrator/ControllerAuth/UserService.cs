using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alpaca.CoverCalibrator
{
    public interface IUserService
    {
        Task<bool> Authenticate(string username, string password);
    }

    public class UserService : IUserService
    {
        //ToDo, don't hard code support multiple users
        public async Task<bool> Authenticate(string username, string password)
        {
            return await Task.Run(() => username == "test" && password == "test" );
        }
    }
}
