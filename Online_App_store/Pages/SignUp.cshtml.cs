using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Online_App_store.models;
using System;
using System.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Online_App_store.Pages
{


    [BindProperties(SupportsGet = true)]
    public class SignUpModel : PageModel
    {
        [BindProperty]
        public new required models.users User { get; set; }
        [BindProperty]
        public required string tempData { get; set; }
        public required string ErrorMessage { get; set; }


        public IActionResult OnPost()
        {
            string connectionString = "Data Source=laptop-oh72tn5u; Initial Catalog = OnlineAppStore; Integrated Security = True";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Insert the user data into the database
                string query = $"insert into Users (UserName, Email, Password, PhoneNumber, Type) " +
               $"values('{User.Name}', '{User.Email}', '{User.Password}', '{User.PhoneNumber}', '{User.Type}')";

                SqlCommand command = new SqlCommand(query, con);

                try
                {
                    con.Open();
                    command.ExecuteNonQuery();

                    return RedirectToPage("/Login");


                }
                catch (SqlException err)
                {
                    Console.WriteLine(err.Message);
                }
                finally
                {
                    con.Close();
                }

                return Page();

            }

        }
        public void OnGet()
        {

        }
    }
}