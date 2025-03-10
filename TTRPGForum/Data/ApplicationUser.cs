using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TTRPGForum.Data
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData] // property is included in download of personal data.
        [Required]
        public string Name { get; set; } = string.Empty;

        [PersonalData]
        public string Location { get; set; } = string.Empty;

        [PersonalData]
        public string ImageFileName { get; set; } = string.Empty;

        [NotMapped]
        public IFormFile ImageFile { get; set; } = null;
    }
}
