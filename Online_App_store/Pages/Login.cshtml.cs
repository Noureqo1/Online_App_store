using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Online_App_store.models;

namespace Online_App_store.Pages
{

    [BindProperties(SupportsGet = true)]
    public class LoginModel : PageModel
    {
        [BindProperty]
        public new required models.users User { get; set; }

        public required string ErrorMessage { get; set; }
        public required string Type { get; set; }

        public IActionResult OnPost()
        {
            // Connect to the database
            string connectionString = "Data Source=laptop-oh72tn5u; Initial Catalog = OnlineAppStore; Integrated Security = True";
            SqlConnection con = new SqlConnection();
            {
                con.ConnectionString = connectionString;

                // Query the database to check if the user credentials match
                string query = $"SELECT * FROM users WHERE Password={User.Password}";
                string queryType = $"SELECT Type FROM users WHERE Password={User.Password}";
                SqlCommand command = new SqlCommand(query, con);
                SqlCommand Getype = new SqlCommand(queryType, con);
                DataTable dt = new DataTable();

                try
                {
                    con.Open();
                    dt.Load(command.ExecuteReader());
                    Type = (string)Getype.ExecuteScalar();
                    if (dt.Rows.Count >= 0)
                    {
                        if (Type == "developer")
                        {                      
                            return RedirectToPage("/Developer");
                        }
                        if (Type == "customer")
                        {
                            return RedirectToPage("/Customer");
                        }
                        if (Type == "admin")
                        {
                            return RedirectToPage("/admin");
                        }

                    }
                    else
                    {
                        // User credentials do not match, display an error message
                        ErrorMessage = "Invalid credentials. Please try again.";
                    }
                    return RedirectToPage("/Explore");


                }
                catch (SqlException err)
                {
                    Console.WriteLine(err.Message);
                }
                finally
                {
                    con.Close();
                }


            }

            return Page();
        }
        public void OnGet()
        {

        }
    }
}