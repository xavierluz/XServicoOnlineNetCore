using Services.cadastro.empresa;
using Services.seguranca;
using Services.seguranca.hash;
using ServicesInterfaces.cadastro;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Services.cadastro
{
    public class CadastroFactory
    {
        private CadastroFactory() { }
        public static CadastroFactory GetInstance()
        {
            return new CadastroFactory();
        }

        public EmpresaAbstract CreateEmpresa(IsolationLevel isolationLevel)
        {
            return EmpresaBusiness.GetInstance(isolationLevel);
        }
        public CriptografiaStrategy CreateAesCriptografia(IEmpresa empresa)
        {
            return AesCriptografia.CreateKeyEmpresa(empresa);
        }
    }
}
