using System.Data.Entity;
using System.Data.Entity.SqlServer;


namespace MyHappyDays.DAL
{
    public class MyHappyDaysConfiguration:DbConfiguration
    {

        public MyHappyDaysConfiguration()
        {
            SetExecutionStrategy("System.Data.SqlClient"), () => new SqlAzureExecutionStrategy());
        }
    }
}