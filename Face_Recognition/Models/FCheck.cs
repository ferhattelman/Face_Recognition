using Microsoft.Data.SqlClient;

namespace Face_Recognition.Models
{
    public class FCheck
    {
        SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-43SO5ER;Initial Catalog=FaceRecognition;TrustServerCertificate=True;Integrated Security=True");
        public string Control(string email)
        {
            string pass = string.Empty;
            connection.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM Login WHERE Email = @p1", connection);
            command.Parameters.AddWithValue("@p1", email);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                pass = reader["Password"].ToString();
                return pass;
            }
            else
            {
                return null;
            }
        }
    }
}
