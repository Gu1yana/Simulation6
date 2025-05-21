using System.ComponentModel.DataAnnotations;

namespace Simulation6.ViewModels.AccountVM
{
    public class LoginVM
    {
        [MaxLength(64)]
        public string UsernameOrEmail { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}