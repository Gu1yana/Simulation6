using Simulation6.Models;
using Simulation6.ViewModels.CommonVM;

namespace Simulation6.ViewModels.PeopleVM
{
    public class PeopleUpdateVM:BaseVM
    {
        public string Fullname { get; set; }
        public string Description { get; set; }
        public string? ImageUrl { get; set; }
        public int ProfessionId { get; set; }
        public IFormFile? ImageFile { get; set; }

    }
}
