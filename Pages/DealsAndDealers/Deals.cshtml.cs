using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace CompanyWebApplication.Pages.DealsAndDealers
{
    public class DealsModel : PageModel
    {
        public List<DealInfo> listDeals = new List<DealInfo>();
        public void OnGet()
        {
            try 
            {
                String connectionString = "Data Source=44-9\\SQLEXPRESS;Initial Catalog=Companydb;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Deal";
                    using(SqlCommand command = new SqlCommand(sql, connection)) 
                    {
                        using (SqlDataReader reader = command.ExecuteReader()) 
                        {
                            while (reader.Read()) 
                            {
                                DealInfo dealInfo = new DealInfo();
                                dealInfo.id = "" + reader.GetInt32(0);
                                dealInfo.name = reader.GetString(1);
                                dealInfo.kolvo = reader.GetInt32(2).ToString();
                                dealInfo.Price = reader.GetDouble(3).ToString();
                                dealInfo.Sum = reader.GetDouble(4).ToString();
                                dealInfo.DateD = reader.GetDateTime(5).ToString();
                                dealInfo.IdDealer = reader.GetInt32(6).ToString();

                                listDeals.Add(dealInfo);
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


    public class DealInfo 
    {
        public String id;
        public String name;
        public String kolvo;
        public String Price;
        public String Sum;
        public String DateD;
        public String IdDealer;
    }
}
