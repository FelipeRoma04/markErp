using System;

namespace Proyecto.Controler
{
    public static class UserSession
    {
        public static int Id { get; set; }
        public static string Username { get; set; }
        public static string Role { get; set; }

        public static bool IsAdmin => Role == "Admin";
        public static string Current => string.IsNullOrEmpty(Username) ? "sistema" : Username;
        
        public static void Logout()
        {
            Id = 0;
            Username = string.Empty;
            Role = string.Empty;
        }
    }
}
