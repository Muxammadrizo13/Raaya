using ECommerce.Domain.Commons;
using ECommerce.Domain.Enums;
using Raaya.Domain.Enums;

namespace ECommerce.Domain.Entities
{
    public class User : Auditable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
        public int height { get; set; }
        public int weight { get; set; }
        public long CartId { get; set; }
        public UserGender Gender { get; set; }

    }
}
