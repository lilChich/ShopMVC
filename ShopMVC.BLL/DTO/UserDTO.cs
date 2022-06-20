using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMVC.BLL.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }

        public DateTime? DateOfBirth { get; set; }
        public string Image { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmedEmail { get; set; }
        public bool RememberMe { get; set; }

        public string Token { get; set; }
        public string Role { get; set; }
    }
}
