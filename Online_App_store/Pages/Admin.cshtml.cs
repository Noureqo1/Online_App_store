using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Online_App_store.models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Online_App_store.Pages
{
    public class AdminModel : PageModel
    {
        public List<users> Users { get; set; }

        [BindProperty]
        public int UserId { get; set; }
        [BindProperty]
        public string UserType { get; set; }

        public void OnGet()
        {
            LoadUsers();
        }

        public IActionResult OnPostDelete(int userId)
        {
            DeleteUser(userId);
            LoadUsers();
            return Page();
        }

        public IActionResult OnPostUpdateType(int userId, string userType)
        {
            UpdateUserType(userId, userType);
            LoadUsers();
            return Page();
        }

        private void LoadUsers()
        {
            Users = new List<users>();
            string connectionString = "Data Source=laptop-oh72tn5u;Initial Catalog=OnlineAppStore;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT Id, Email, UserName, Type, PhoneNumber FROM Users";  // Ensure these columns exist in your database
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Users.Add(new users
                            {
                                Id = reader.GetInt32(0),
                                Email = reader.GetString(1),
                                Name = reader.GetString(2),
                                Type = reader.GetString(3),
                                PhoneNumber = reader.GetInt32(4)
                            });
                        }
                    }
                }
            }
        }

        private void DeleteUser(int userId)
        {
            string connectionString = "Data Source=laptop-oh72tn5u;Initial Catalog=OnlineAppStore;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "DELETE FROM Users WHERE Id = @UserId";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.ExecuteNonQuery();
                }
            }
        }

        private void UpdateUserType(int userId, string userType)
        {
            string connectionString = "Data Source=laptop-oh72tn5u;Initial Catalog=OnlineAppStore;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "UPDATE Users SET Type = @UserType WHERE Id = @UserId";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@UserType", userType);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
