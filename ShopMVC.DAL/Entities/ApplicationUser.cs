using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMVC.DAL.Entities
{
    public class ApplicationUser : IdentityUser<int>
    {
        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(30)]
        public string SecondName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Image { get; set; }

        public IList<Purchase> Purchases { get; set; }

        public ApplicationUser()
        {

        }
    }
}
