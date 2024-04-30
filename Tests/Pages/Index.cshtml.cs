using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tests.Data;
using Tests.Models;

namespace Tests.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private int tagsCount = 6;
        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
            tagsCount = 10; 

        }
        public int CountProp { get; set; }
        public ICollection<Test> Tests { get; set; }
        public ICollection<Test> RandomTests { get; set; }

        public void OnGet()
        {
            CountProp = _context.Tests.Count();
            Tests = _context.Tests.ToList();
            RandomTests = GetRandomElements(Tests, tagsCount).ToList();
        }

        //перенести потом в отдельную библитеку
        public static IEnumerable<T> GetRandomElements<T>(IEnumerable<T> collection, int count)
        {
            var random = new Random();
            var shuffledCollection = collection.OrderBy(x => random.Next()).ToList();
            return shuffledCollection.Take(count);
        }
    }

}