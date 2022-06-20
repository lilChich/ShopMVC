using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopMVC.ViewModels
{
    public class EditUserViewModel
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string SecondName { get; set; }
     
        public DateTime? DayOfBirth { get; set; }
        [Required]
        public string ExistingPhotoPath { get; set; }
        [Display(Name = "Image")]
        public IFormFile Image { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
