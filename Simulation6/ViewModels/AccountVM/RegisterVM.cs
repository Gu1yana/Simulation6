using System.ComponentModel.DataAnnotations;

namespace Simulation6.ViewModels.AccountVM
{
    public class RegisterVM
    {
        [MaxLength(64)]
        public string Fullnmae { get; set; }
        [ MaxLength(128)]
        public string Username { get; set; }
        [MaxLength(128), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password), Compare(nameof(Password))]
        public string RepeatPassword { get; set; }
    }
}