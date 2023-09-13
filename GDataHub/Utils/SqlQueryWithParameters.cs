using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDataHub.Utils
{
    public class SqlQueryWithParameters
    {
        public string sqlString;
        public IDataParameter[] parameters;
    }
}
