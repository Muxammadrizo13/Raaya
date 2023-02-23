using Ecommerce.Sevice.DTOs;
using Raaya.Domain.Enums;
using System;


namespace Ecommerce.Sevice.Helpers
{
    public class UserCreationDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Login { get; set; }
        public string Phone { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public CartCreationDto Cart { get; set; }
        public UserGender Gender { get; set; }
    }
}
