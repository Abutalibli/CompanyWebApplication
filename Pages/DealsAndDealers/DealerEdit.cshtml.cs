using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace CompanyWebApplication.Pages.DealsAndDealers
{
    public class DealerEditModel : PageModel
    {
        public DealerInfo dealinfo = new DealerInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {
                String connectionString = "Data Source=44-9\\SQLEXPRESS;Initial Catalog=Companydb;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Dealer WHERE IdDealer=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                dealinfo.id = "" + reader.GetInt32(0);
                                dealinfo.fio = reader.GetString(1);
                                dealinfo.passportnumber = reader.GetString(2);
                                dealinfo.phonenumber = reader.GetString(3);
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        public void OnPost() 
        {
            dealinfo.id = Request.Form["id"];
            dealinfo.fio = Request.Form["fio"];
            dealinfo.passportnumber = Request.Form["passportnumber"];
            dealinfo.phonenumber = Request.Form["phonenumber"];


            if (dealinfo.id.Length == 0 || dealinfo.fio.Length == 0 || dealinfo.passportnumber.Length == 0 ||
                dealinfo.phonenumber.Length == 0)
            {
                errorMessage = "Все поля должны быть заполнены";
                return;
            }

            try
            {
                String connectionString = "Data Source=44-9\\SQLEXPRESS;Initial Catalog=Companydb;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE Dealer " +
                        "SET FIO=@fio, PassportNumber=@passportnumber, PhoneNumber=@phonenumber " +
                        "WHERE IdDealer=@id ";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("id", dealinfo.id);
                        command.Parameters.AddWithValue("@fio", dealinfo.fio);
                        command.Parameters.AddWithValue("@passportnumber", dealinfo.passportnumber);
                        command.Parameters.AddWithValue("@phonenumber", dealinfo.phonenumber);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/DealsAndDealers/Deals");
        }
    }
}
