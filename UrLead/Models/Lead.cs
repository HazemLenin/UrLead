using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace UrLead.Models
{
    public class Lead
    {
        public int LeadId { get; set; }

        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }

        public string? Description { get; set; }

        [Required]
        [Range(0, 100)]
        public int Probability { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public virtual LeadCategory? Category { get; set; }

        [Required]
        public string? OrganizationId { get; set; }

        public virtual IdentityUser? Organization { get; set; }
    }
}
