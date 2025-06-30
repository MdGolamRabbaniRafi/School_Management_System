using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class HealthCheckServices
    {
        public static string getHealthHealthCheck()
        {
            return DataAccessFactory.healthCheakData().getHealthHealthCheck();
        }
    }
}
