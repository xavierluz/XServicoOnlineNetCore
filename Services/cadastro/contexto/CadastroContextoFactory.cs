using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Services.bases;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.cadastro.contexto
{
    internal class CadastroContextoFactory : IDesignTimeDbContextFactory<CadastroContexto>
    {
        public CadastroContexto CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CadastroContexto>();
            optionsBuilder.UseNpgsql(PostgreSqlFactory.GetInstance().GetConnection());

            return CadastroContexto.GetInstance(optionsBuilder.Options);
        }
    }
}
