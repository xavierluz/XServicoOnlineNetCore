using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Services.bases;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.movimento.contexto
{
    internal class MovimentoContextoFactory : IDesignTimeDbContextFactory<MovimentoContexto>
    {
        public MovimentoContexto CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MovimentoContexto>();
            optionsBuilder.UseNpgsql(PostgreSqlFactory.GetInstance().GetConnection());

            return MovimentoContexto.GetInstance(optionsBuilder.Options);
        }
    }
}
