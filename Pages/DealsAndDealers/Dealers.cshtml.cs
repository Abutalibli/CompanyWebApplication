using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace CompanyWebApplication.Pages.DealsAndDealers
{
    public class DealersModel : PageModel
    {
        public List<DealerInfo> listDealers = new List<DealerInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=44-9\\SQLEXPRESS;Initial Catalog=Companydb;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Dealer";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DealerInfo dealerInfo = new DealerInfo();
                                dealerInfo.id = "" + reader.GetInt32(0);
                                dealerInfo.fio = reader.GetString(1);
                                dealerInfo.passportnumber = reader.GetString(2);
                                dealerInfo.phonenumber = reader.GetString(3);

                                listDealers.Add(dealerInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }


    public class DealerInfo
    {
        public String id;
        public String fio;
        public String passportnumber;
        public String phonenumber;
    }
}
