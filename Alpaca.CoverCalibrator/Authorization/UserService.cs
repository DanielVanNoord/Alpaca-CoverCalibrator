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
            return await Task.Run(() =>
            {
                try
                {
                    return username == AlpacaSettings.UserName && Hash.Validate(AlpacaSettings.Password, password);
                }
                catch
                {
                    return false;
                }
            }

            );
        }
    }
}