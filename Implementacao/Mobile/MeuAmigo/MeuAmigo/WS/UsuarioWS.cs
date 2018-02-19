using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeuAmigo.Model;

namespace MeuAmigo.WS
{
    public class UsuarioWS
    {
        public static async Task<List<Usuario>> SelecionarContatos(Usuario usuarioLogado)
        {
            try
            {
                return new List<Usuario>();
            }
            catch (Exception e)
            {
                
                throw;
            }
        }
    }
}
