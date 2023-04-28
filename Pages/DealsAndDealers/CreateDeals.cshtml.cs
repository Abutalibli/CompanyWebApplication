using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace CompanyWebApplication.Pages.DealsAndDealers
{
    public class CreateDealsModel : PageModel
    {
        public DealInfo dealInfo = new DealInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost() 
        {
            dealInfo.name = Request.Form["name"];
            dealInfo.kolvo = Request.Form["kolvo"];
            dealInfo.Price = Request.Form["price"];
            dealInfo.Sum = Request.Form["sum"];
            dealInfo.DateD = Request.Form["date"];
            dealInfo.IdDealer = Request.Form["iddealer"];

            if (dealInfo.name.Length == 0 || dealInfo.kolvo.Length == 0 ||
                dealInfo.Price.Length == 0 || dealInfo.Sum.Length == 0 ||
                dealInfo.DateD.Length == 0 || dealInfo.IdDealer.Length == 0)
            {
                errorMessage = "¬се пол€ должны быть заполнены";
                return;
            }

            //сохранение новой сделки в базу данных
            try
            {
                String connectionString = "Data Source=44-9\\SQLEXPRESS;Initial Catalog=Companydb;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO Deal " +
                        "(GoodName, Goodkolvo, GoodPrice, Sum, data_deal, iddealer) VALUES" +
                        "(@name, @kolvo, @Price, @Sum, @DateD, @iddealer);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", dealInfo.name);
                        command.Parameters.AddWithValue("@kolvo", dealInfo.kolvo);
                        command.Parameters.AddWithValue("@Price", dealInfo.Price);
                        command.Parameters.AddWithValue("@Sum", dealInfo.Sum);
                        command.Parameters.AddWithValue("@DateD", dealInfo.DateD);
                        command.Parameters.AddWithValue("@iddealer", dealInfo.IdDealer);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex) 
            {
                errorMessage = ex.Message;
                return;
            }

            dealInfo.name = ""; dealInfo.kolvo = ""; dealInfo.Price = ""; dealInfo.Sum = ""; dealInfo.DateD = ""; dealInfo.IdDealer = "";
            successMessage = "Ќова€ сделка добавлена";

            Response.Redirect("/DealsAndDealers/Deals");
        }
    }
}
