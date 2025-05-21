using Simulation6.Models;
using Simulation6.ViewModels.CommonVM;
using Simulation6.ViewModels.ProfessionVM;

namespace Simulation6.ViewModels.PeopleVM
{
    public class PeopleGetVM:BaseVM
    {
        public string Fullname { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int ProfessionId { get; set; }
        public ProfessionGetVM Profession { get; set; }
    }
}
