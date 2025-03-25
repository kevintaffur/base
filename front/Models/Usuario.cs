namespace front.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public virtual Rol Rol { get; set; }
    }
}
