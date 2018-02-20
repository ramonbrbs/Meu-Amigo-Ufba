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
        public static async Task<Usuario> Cadastrar(Usuario usuario)
        {
            try
            {
                string url = Constantes.URL_API + "usuarios";
                var req = new Request(url);
                return await req.Post<Usuario>(usuario);
            }
            catch (Exception e)
            {
                
                throw;
            }
        }

        public class InteresseVM
        {
            public int idUsuario { get; set; }
            public int idInteresse { get; set; }
        }

        public static async Task<InteresseVM> CadastrarInteresses(UsuarioInteresse ui)
        {

            try
            {
                string url = Constantes.URL_API + "interesses";
                var req = new Request(url);
                return await req.Post<InteresseVM>(ui);

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static async Task<InteresseVM> CadastrarLinguas(UsuarioLingua ul)
        {

            try
            {
                string url = Constantes.URL_API + "linguas";
                var req = new Request(url);
                return await req.Post<InteresseVM>(ul);

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public class LoginVM
        {
            public string Email { get; set; }
            public string Senha { get; set; }
        }

        public static async Task<Usuario> Login(LoginVM login)
        {
            try
            {
                //string cnpj, string cpf, DateTime? mes, DateTime? inicioPeriodo, DateTime? fimPeriodo, int convenioId, int //hospitalId, string paciente, int? situacaoId

                string url = Constantes.URL_API + $"login?email={login.Email}&senha={login.Senha}";
                var req = new Request(url);
                return await req.Get<Usuario>();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static async Task<List<Usuario>> GetContatos(int id)
        {
            try
            {
                //string cnpj, string cpf, DateTime? mes, DateTime? inicioPeriodo, DateTime? fimPeriodo, int convenioId, int //hospitalId, string paciente, int? situacaoId

                string url = Constantes.URL_API + $"contatos?id={id}";
                var req = new Request(url);
                return await req.Get<List<Usuario>>();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
