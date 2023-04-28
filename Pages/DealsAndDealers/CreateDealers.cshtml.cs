using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace CompanyWebApplication.Pages.DealsAndDealers
{
    public class CreateDealersModel : PageModel
    {
        public DealerInfo dealerInfo = new DealerInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            dealerInfo.fio = Request.Form["fio"];
            dealerInfo.passportnumber = Request.Form["passportnumber"];
            dealerInfo.phonenumber = Request.Form["phonenumber"];

            if (dealerInfo.fio.Length == 0 || dealerInfo.passportnumber.Length == 0 ||
                dealerInfo.phonenumber.Length == 0)
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
                    String sql = "INSERT INTO Dealer " +
                        "(FIO, PassportNumber, PhoneNumber) VALUES" +
                        "(@fio, @passportnumber, @phonenumber);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@fio", dealerInfo.fio);
                        command.Parameters.AddWithValue("@passportnumber", dealerInfo.passportnumber);
                        command.Parameters.AddWithValue("@phonenumber", dealerInfo.phonenumber);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            dealerInfo.fio = ""; dealerInfo.passportnumber = ""; dealerInfo.phonenumber = "";
            successMessage = "Ќова€ сделка добавлена";

            Response.Redirect("/DealsAndDealers/Dealers");
        }
    }
}
