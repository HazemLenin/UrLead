using System.ComponentModel.DataAnnotations;

namespace UrLead.Models
{
    public class LeadCategory
    {
        public int LeadCategoryId { get; set; }

        [Required]
        public string Title { get; set; }

        public virtual List<Lead>? Leads { get; set; }
    }
}
