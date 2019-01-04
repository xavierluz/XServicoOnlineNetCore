using Services.produto.categoria;
using Services.produto.classificacao;
using Services.produto.material;
using ServicesInterfaces.produto;
using System.Data;

namespace Services.produto
{
    public class ProdutoFactory
    {
        private ProdutoFactory() { }
        public static ProdutoFactory GetInstance()
        {
            return new ProdutoFactory();
        }

        public CategoriaAbstract CreateCategoria(IsolationLevel isolationLevel)
        {
            return CategoriaBusiness.GetInstancia(isolationLevel);
        }
        public ClassificacoAbstract CreateClassificao(IsolationLevel isolationLevel)
        {
            return ClassificacaoBusiness.GetInstancia(isolationLevel);
        }
        public MaterialAbstract CreateMaterial(IsolationLevel isolationLevel)
        {
            return MaterialBusiness.GetInstancia(isolationLevel);
        }
    }
}
