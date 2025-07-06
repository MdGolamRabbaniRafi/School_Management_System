using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repos;

namespace DAL
{
    public class DataAccessFactory
    {
        public static IHealthCheck<string> healthCheakData()
        {
            return new HealthCheckRepo();
        }
    }
}
