using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace CompanyWebApplication.Pages.DealsAndDealers
{
    public class EditDealsModel : PageModel
    {
        public DealInfo dealinfo = new DealInfo();
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
                    String sql = "SELECT * FROM Deal WHERE Id_Deal=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if(reader.Read()) 
                            {
                                dealinfo.id = "" + reader.GetInt32(0);
                                dealinfo.name = reader.GetString(1).ToString();
                                dealinfo.kolvo = reader.GetInt32(2).ToString();
                                dealinfo.Price = reader.GetDouble(3).ToString();
                                dealinfo.Sum = reader.GetDouble(4).ToString();
                                dealinfo.DateD = reader.GetDateTime(5).ToString();
                                dealinfo.IdDealer = reader.GetInt32(6).ToString();
                            }
               
                        }
                    }
                }
            }
            catch(Exception ex) 
            {

            }
        }

        public void OnPost() 
        {
            dealinfo.id = Request.Form["id"];
            dealinfo.name = Request.Form["name"];
            dealinfo.kolvo = Request.Form["kolvo"];
            dealinfo.Price = Request.Form["price"];
            dealinfo.Sum = Request.Form["sum"];
            dealinfo.DateD = Request.Form["date"];
            dealinfo.IdDealer= Request.Form["iddealer"];

            if (dealinfo.id.Length == 0 || dealinfo.name.Length == 0 || dealinfo.kolvo.Length == 0 ||
                dealinfo.Price.Length == 0 || dealinfo.Sum.Length == 0 ||
                dealinfo.DateD.Length == 0 || dealinfo.IdDealer.Length == 0)
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
                    String sql = "UPDATE Deal " +
                        "SET GoodName=@name, Goodkolvo=@kolvo, GoodPrice=@price, Sum=@sum,data_deal=@date ,iddealer=@iddealer " +
                        "WHERE Id_Deal=@id ";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("id", dealinfo.id);
                        command.Parameters.AddWithValue("@name", dealinfo.name);
                        command.Parameters.AddWithValue("@kolvo", dealinfo.kolvo);
                        command.Parameters.AddWithValue("@price", dealinfo.Price);
                        command.Parameters.AddWithValue("@sum", dealinfo.Sum);
                        command.Parameters.AddWithValue("@date", dealinfo.DateD);
                        command.Parameters.AddWithValue("@iddealer", dealinfo.IdDealer);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex) 
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/DealsAndDealers/Deals");

        }
    }
}
