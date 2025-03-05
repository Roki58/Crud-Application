using System.Data;
using System.Data.SqlClient;
using Azure;
using Crud_Application.Models;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Crud_Application.Repositories
{
    public class UserRepo : IUserRepo
    {
        private readonly IConfiguration _configuration;
        private object cmd;

        public SqlCommand SqlCommand { get; private set; }

        public UserRepo(IConfiguration configuration)
        {
            _configuration = configuration; 
        }
        public async Task<int> CreateUserAsync(User user)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("CrudConnection").ToString());
            SqlCommand cmd = new SqlCommand("INSERT INTO DemoUser(Name,Password,Email,IsActive) VALUES('" + user.Name + "','" + user.Password + "','" + user.Email + "','" + user.IsActive + "') ", connection);
            connection.Open();
            int rowEffected =cmd.ExecuteNonQuery();
            connection.Close();
            return  rowEffected > 0 ? 1 : 0;
          
        }
        public async Task<ResponseDto> GetAllUsersAsync()
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("CrudConnection").ToString());
            SqlDataAdapter da = new SqlDataAdapter("Select * From DemoUser where IsActive = 1", connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<User> users = new List<User>();
            ResponseDto response = new ResponseDto();
            if (dt.Rows.Count > 0)
            {
                for(int i = 0; i < dt.Rows.Count; i++)
                {
                    User user = new User();
                    user.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                    user.Name = Convert.ToString(dt.Rows[i]["Name"]);

                    user.Email = Convert.ToString(dt.Rows[i]["Email"]);

                    user.IsActive = Convert.ToInt32(dt.Rows[i]["IsActive"]);
                    users.Add(user);

                }
                response.StatusCode = "201";
                response.Msg = "Succesfully get All user";
                response.Data = users;

            }
            else
            {
                response.StatusCode = "201";
                response.Msg = "Empty user";
               


            }
            return response;


        }
        public async Task<ResponseDto> GetUserByIdAsync(int id)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("CrudConnection").ToString());
            SqlDataAdapter da = new SqlDataAdapter("Select * From DemoUser WHERE ID ='" + id + "'AND IsActive= 1 ", connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            ResponseDto response=new ResponseDto();
            if(dt.Rows.Count > 0)
            {
                User user=new User();
                user.Id = Convert.ToInt32(dt.Rows[0]["Id"]);
                user.Name = Convert.ToString(dt.Rows [0]["Name"]);
                user.Email = Convert.ToString(dt.Rows[0]["Email"]);
                user.IsActive = Convert.ToInt32(dt.Rows[0]["IsActive"]);

                response.StatusCode = "200";
                response.Msg = "user  found";
                response.Data = user;

            }
            else
            {
                response.StatusCode = "201";
                response.Msg = "user not found";

            }
            return response;

        }
        public async Task<ResponseDto> UpdatedUserAsync(User user)
        {
            ResponseDto response = new ResponseDto();
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("CrudConnection").ToString()))
            {
                string query = "UPDATE DemoUser SET Name=@Name, Email=@Email WHERE Id=@Id";
                connection.Open();
                int roweffected = await connection.ExecuteAsync(query, new { Name = user.Name, Email = user.Email, Id = user.Id });
                connection.Close();
                if (roweffected > 0)
                {
                    response.Msg = "Succesfully Upadated";
                    response.StatusCode = "200";
                    response.Data= user;
                }
                else
                {

                    response.Msg = "Succesfully not Upadated";
                    response.StatusCode = "500";
                   

                }
                
            }
            return response;
        }
        public async Task<ResponseDto> DeleteUserByIdAsync(int id)
        {
            string query = "DELETE FROM DemoUser WHERE ID = @Id";

            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("CrudConnection")))
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                   
                    cmd.Parameters.AddWithValue("@Id", id);

                    await connection.OpenAsync();
                    int rowsAffected = await cmd.ExecuteNonQueryAsync(); 
                    ResponseDto response = new ResponseDto();

                    if (rowsAffected > 0)
                    {
                        response.StatusCode = "200";
                        response.Msg = "User successfully deleted";
                    }
                    else
                    {
                        response.StatusCode = "500";
                        response.Msg = "User not deleted";
                    }

                    return response;
                }
            }
        }

        public async Task<ResponseDto> SoftDeleteByIdAsync(int id)
        {
            ResponseDto response = new ResponseDto();

            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("CrudConnection")))
            {
                string query = @"Update DemoUser set IsActive = 0 Where Id = @Id";
                connection.Open();
                int roweffected= await connection.ExecuteAsync(query, new { Id =id });
                connection.Close();
                if(roweffected > 0)
                {
                    response.Msg = "Succesfully deleted";
                    response.StatusCode = "200";
                }
                else
                {
                    response.Msg = "user not deleted";
                    response.StatusCode = "500";
                }

            }

            return response;
        }
       
    }
}
