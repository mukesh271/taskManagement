using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using task_management.Models;

namespace task_management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController: ControllerBase
    {
        SqlConnection conn;
        private readonly IConfiguration _configuration;
        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost, Route("[action]", Name="Login")]
        public UserDetails Login(Users users)
        {
            UserDetails userDetails= new UserDetails();
            userDetails.result = new Result();
            try
            {
                if (users != null && !string.IsNullOrWhiteSpace(users.username) && !string.IsNullOrWhiteSpace(users.password))
                {
                    conn = new SqlConnection(_configuration["connectionStrings:sql-conn"]);
                    using (conn)
                    {
                        SqlCommand cmd = new SqlCommand("sp_users", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@username", users.username);
                        cmd.Parameters.AddWithValue("@password", users.password);
                        cmd.Parameters.AddWithValue("@stmttype", "userlogin");
                        SqlDataAdapter adapter= new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        if(dt != null && dt.Rows.Count > 0)
                        {
                            userDetails.username = dt.Rows[0]["username"].ToString();
                            userDetails.firstname = dt.Rows[0]["firstname"].ToString();
                            userDetails.lastname = dt.Rows[0]["lastname"].ToString();
                            userDetails.email = dt.Rows[0]["email"].ToString();
                            userDetails.gender = dt.Rows[0]["gender"].ToString();
                            userDetails.contactno = dt.Rows[0]["contactno"].ToString();
                            userDetails.dob = dt.Rows[0]["dob"].ToString();
                            userDetails.role = dt.Rows[0]["role"].ToString();

                            userDetails.result.result = true;
                            userDetails.result.message = "success";
                        }
                        else
                        {
                            userDetails.result.result = false;
                            userDetails.result.message = "Invalid User";
                        }
                    }
                }
                else
                {
                    userDetails.result.result = false;
                    userDetails.result.message = "Please Enter username and password";
                }
            }
            catch(Exception ex)
            {
                userDetails.result.result = false;
                userDetails.result.message = "Error occurred " + ex.Message.ToString();
            }
            return userDetails;
        }

    }
}
