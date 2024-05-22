using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Online_App_store.models
{
    [BindProperties(SupportsGet = true)]
    public class users
    {
        internal int Id;

        [BindProperty]
        [StringLength(25, MinimumLength = 3)]
        public String? Email { get; set; }
        [BindProperty]
        public int? Password { get; set; }
        [BindProperty]
        public int? ConfirmPassword { get; set; }
        [BindProperty]
        public String? Name { get; set; }
        [BindProperty]
        public String? Type { get; set; }
        [BindProperty]
        public int? PhoneNumber { get; set; }
    }
}
