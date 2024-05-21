using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Online_App_store.models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Online_App_store.Pages
{
    public class ProfileModel : PageModel
    {
        public string Email { get; set; }
        [BindProperty]
        public new required models.users User { get; set; }
        // Replace "YourConnectionString" with your actual database connection string

        string connectionString = "Data Source=laptop-oh72tn5u; Initial Catalog = OnlineAppStore; Integrated Security = True";

        public void OnGet()
        {

            // Example: Retrieving user profile data from the database
            string userEmail = "ahmed@hotmail.com"; // Assuming this is the user's email

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT Email, Password, UserName, Type, PhoneNumber FROM Users WHERE Email = @Email";

                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.AddWithValue("@Email", userEmail);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    User.Email = reader["Email"].ToString();
                    User.Password = reader["Password"] as int?;
                    User.Name = reader["UserName"].ToString();
                    User.Type = reader["Type"].ToString();
                    User.PhoneNumber = reader["PhoneNumber"] as int?;
                }
                reader.Close();
            }
        }
    }
}
