using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Services.bases;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.produto.contexto
{
    internal class ProdutoContextoFactory : IDesignTimeDbContextFactory<ProdutoContexto>
    {
        public ProdutoContexto CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ProdutoContexto>();
            optionsBuilder.UseNpgsql(PostgreSqlFactory.GetInstance().GetConnection());

            return ProdutoContexto.GetInstance(optionsBuilder.Options);
        }
    }
}
