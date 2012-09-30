using System.ComponentModel.DataAnnotations;

namespace RogersHouse.WebUI.Models
{
    public class LogOnViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }
}