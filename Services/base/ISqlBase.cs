using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Services.bases
{
    public interface ISqlBase
    {
        DbConnection GetConnection();
    }
}
