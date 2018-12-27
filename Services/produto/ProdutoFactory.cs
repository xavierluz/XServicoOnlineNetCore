using Services.bases;
using Services.produto.categoria;
using ServicesInterfaces.banco;
using ServicesInterfaces.produto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Services.produto
{
    public class ProdutoFactory
    {
        private ProdutoFactory() { }
        public static ProdutoFactory GetInstance()
        {
            return new ProdutoFactory();
        }

        public CategoriaAbstract CreateInstance(IsolationLevel isolationLevel)
        {
            return CategoriaBusiness.GetInstancia(isolationLevel);
        }
    }
}
