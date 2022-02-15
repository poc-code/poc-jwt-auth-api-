namespace SSO.Domain.Model
{
    public class Acesso
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public long AccessKey { get; set;}
        public bool Logged { get; set; }
        public DateTime DateAccess { get; set; }
        
    }
}
