using E_Learning.Data.Model;

namespace E_Learning.Interfaces
{
    public interface ITokenServices
    {
        string GetToken(User user);
    }
}
