using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ser_ver
{
    [Serializable]
    public class Admin
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Password { get; set; }
        /*[Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }*/
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Login { get; set; }

        public Admin() { }

        public (Boolean, List<ValidationResult>) Validation()
        {
            bool correct;
            var context = new ValidationContext(this);
            var result = new List<ValidationResult>();
            if (!Validator.TryValidateObject(this, context, result, true))
            {
                correct = false;
            }
            else
            {
                correct = true;
            }
            return (correct, result);
        }
    }
}
