namespace WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;
    using IRepository;
    using POCO;
    using Repository;
    using Service;

    public class AuthorizeController : ApiController
    {
        [HttpGet]
        public Logon Authenticate(string email, string password)
        {
            var errors = new List<string>();
            IAuthorizeRepository authorize = new AuthorizeRepository();
            return new AuthorizeService(authorize).Authenticate(email, password, ref errors);
        }

        [HttpPost]
        public Logon Authenticate([FromBody]Logon loginInfo)
        {
            var errors = new List<string>();
            IAuthorizeRepository authorize = new AuthorizeRepository();
            return new AuthorizeService(authorize).Authenticate(loginInfo.UserName, loginInfo.Password, ref errors);
        }
    }
}