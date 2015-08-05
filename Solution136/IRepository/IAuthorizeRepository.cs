namespace IRepository
{
    using System.Collections.Generic;

    using POCO;

    public interface IAuthorizeRepository
    {
        Logon Authenticate(string email, string password, ref List<string> errors);
    }
}
