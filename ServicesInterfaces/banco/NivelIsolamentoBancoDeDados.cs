using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ServicesInterfaces.banco
{
    public sealed class NivelIsolamentoBancoDeDados
    {
        private NivelIsolamentoBancoDeDados() { }
        public static IsolationLevel GetLerDadosNaoComitado()
        {
            return IsolationLevel.ReadUncommitted;
        }
        public static IsolationLevel GetLerDadosComitado()
        {
            return IsolationLevel.ReadCommitted;
        }
        public static IsolationLevel GetBloqueiaAtualizacaoDosDadosLindos()
        {
            return IsolationLevel.RepeatableRead;
        }
        public static IsolationLevel GetBloqueiaInclusaoAtualizacaoDosDadosLindos()
        {
            return IsolationLevel.Serializable;
        }
    }
}
