using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Online_App_store.models
{
    [BindProperties(SupportsGet = true)]
    public class App
    {
        [BindProperty]
        public int Id { get; set; }
        [BindProperty]
        [StringLength(25, MinimumLength = 3)]
        public String? AppName { get; set; }
        [BindProperty]
        public String? DevName { get; set; }
        [BindProperty]
        public String? Type { get; set; }
        [BindProperty]
        public String? AppFile { get; set; }
        [BindProperty]
        public IFormFile? UploadedFile { get; set; }
    }
}