namespace SSO.Domain.Model
{
    public class Usuario
    {
        public string ID {get; set;}
        public string AccessKey {get; set;}
        public string Email {get; set;}
        public string Password {get; set;}
        public string Role {get; set;}
    }
}