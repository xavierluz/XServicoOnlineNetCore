using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Services.bases
{
    public class PostgreSqlFactory : ISqlBase
    {
        private NpgsqlConnection conncetion;
        private PostgreSqlFactory()
        {
            this.conncetion = new NpgsqlConnection(GetConnectionString());
        }

        public DbConnection GetConnection()
        {
            return this.conncetion;
        }

        public static PostgreSqlFactory GetInstance()
        {
            return new PostgreSqlFactory();
        }
        private string GetConnectionString()
        {
            return "Server=localhost;Port=5432;Database=OnlineETServico;User Id=postgres;Password=lugo2012;";
        }
    }
}
