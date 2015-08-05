namespace POCO
{
    public class Logon
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Id { get; set; }

        public string Role { get; set; }

        public override string ToString()
        {
            return this.UserName + "-" + this.Id + "-" + this.Role;
        }
    }
}
