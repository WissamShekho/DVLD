using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer
{
    static class DataAccessLayerSettings
    {
        public static string ConnectionString()
        {
            return "Data source=.;initial catalog=DVLD;Integrated security=true;TrustServerCertificate=True";
        }
    }
}
