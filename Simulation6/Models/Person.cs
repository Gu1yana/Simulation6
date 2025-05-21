using Simulation6.Models.Common;

namespace Simulation6.Models;

public class Person:BaseEntity
{
    public string Fullname { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public int ProfessionId { get; set; }
    public Profession Profession { get; set; }
}