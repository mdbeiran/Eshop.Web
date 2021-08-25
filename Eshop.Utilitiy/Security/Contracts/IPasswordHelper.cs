namespace Eshop.Utilitiy.Security.Contracts
{
    public interface IPasswordHelper
    {
        string EncodePasswordMd5(string password);
    }
}
