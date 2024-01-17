namespace DiverseMarket.Backend.DTOs.Moderator
{
    public class UserDTO
    {
        public long UserId { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Role { get; set; }

        public UserDTO(long userId, string name, string username, string password, string email, string telephone, string role)
        {
            UserId = userId;
            Name = name;
            Username = username;
            Password = password;
            Email = email;
            Telephone = telephone;
            Role = role;
        }
    }
}
