namespace Service
{
    using System;
    using System.Collections.Generic;
    using IRepository;
    using POCO;

    public class AuthorizeService
    {
        private readonly IAuthorizeRepository repository;

        public AuthorizeService(IAuthorizeRepository repository)
        {
            this.repository = repository;
        }

        public Logon Authenticate(string email, string password, ref List<string> errors)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                errors.Add("Invalid email or password.");
                throw new ArgumentException();
            }

            return this.repository.Authenticate(email, password, ref errors);
        }
    }
}
