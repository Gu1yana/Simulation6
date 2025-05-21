namespace Simulation6.ViewModels.PeopleVM
{
    public class PeopleCreateVM
    {
        public IFormFile ImageFile { get; set; }
        public string Fullname { get; set; }
        public string Description { get; set; }
        public int ProfessionId { get; set; }
    }
}