using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IsqlDataHelper
    {
        Task<int> ExcuteNonQueryasync(SqlCommand cmd);
        Task<DataTable> SqlDataAdapterasync(SqlCommand cmd);
    }
}
