using UrLead.Data;
using UrLead.Models;

namespace UrLead.Seeds
{
    public class DefaultLeadCategoriesSeed
    {

        public async Task SeedAsync(ApplicationDbContext context)
        {
            Console.WriteLine("Seeding default lead categories...");
            string[] categoriesTitles = { "New", "Contacted", "Converted", "Unconverted" };
            List<LeadCategory> categories = new List<LeadCategory>();

            foreach(string category in categoriesTitles)
            {
                categories.Add(new LeadCategory()
                {
                    Title = category,
                });
            }

            await context.LeadCategory.AddRangeAsync(categories);
            await context.SaveChangesAsync();
        }
    }
}
