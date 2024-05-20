using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;

namespace Online_App_store.models
{
    public class Products
    {
        [Key]
        [BindProperty]
        public int Id { get; set; }

        [Required]
        [BindProperty]
        [DisplayName("Product name")]
        public string productName { get; set; }
        [Required]
        [BindProperty]
        public double price { get; set; }
        [Required]
        [BindProperty]
        public int quantity { get; set; }
        [BindProperty]
        public int Size { get; set; }
        [BindProperty]
        public int Stock { get; set; }
        [BindProperty]
        public required string Discription { get; set; }

    }
}