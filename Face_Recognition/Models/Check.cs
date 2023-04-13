using Microsoft.Data.SqlClient;

namespace Face_Recognition.Models
{
    public class Check
    {
        SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-43SO5ER;Initial Catalog=FaceRecognition;TrustServerCertificate=True;Integrated Security=True");

        public bool Control(string user, string pass)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("Select * From tbl_login where Username=@p1 AND Password=@p2", connection);
            command.Parameters.AddWithValue("@p1", user);
            command.Parameters.AddWithValue("@p2", pass);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
