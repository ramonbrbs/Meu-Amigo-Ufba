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

        public static async Task<List<Usuario>> GetContatosPendentes(int id)
        {
            try
            {
                //string cnpj, string cpf, DateTime? mes, DateTime? inicioPeriodo, DateTime? fimPeriodo, int convenioId, int //hospitalId, string paciente, int? situacaoId

                string url = Constantes.URL_API + $"contatos_pendentes?id={id}";
                var req = new Request(url);
                return await req.Get<List<Usuario>>();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static async Task<List<Usuario>> GetBuscaContatos(int id)
        {
            try
            {
                //string cnpj, string cpf, DateTime? mes, DateTime? inicioPeriodo, DateTime? fimPeriodo, int convenioId, int //hospitalId, string paciente, int? situacaoId

                string url = Constantes.URL_API + $"buscar_contatos?id={id}";
                var req = new Request(url);
                return await req.Get<List<Usuario>>();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static async Task<List<Lingua>> GetLinguaUsuario(int id)
        {
            try
            {
                //string cnpj, string cpf, DateTime? mes, DateTime? inicioPeriodo, DateTime? fimPeriodo, int convenioId, int //hospitalId, string paciente, int? situacaoId

                string url = Constantes.URL_API + $"linguas_usuario?id={id}";
                var req = new Request(url);
                return await req.Get<List<Lingua>>();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static async Task<List<Interesse>> GetInteresseUsuario(int id)
        {
            try
            {
                //string cnpj, string cpf, DateTime? mes, DateTime? inicioPeriodo, DateTime? fimPeriodo, int convenioId, int //hospitalId, string paciente, int? situacaoId

                string url = Constantes.URL_API + $"interesses_usuario?id={id}";
                var req = new Request(url);
                return await req.Get<List<Interesse>>();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public class SolicitarVM
        {
            public int id { get; set; }
            public int id2 { get; set; }
        }
        public static async Task<Object> SolicitarContato(int id)
        {
            try
            {
                //string cnpj, string cpf, DateTime? mes, DateTime? inicioPeriodo, DateTime? fimPeriodo, int convenioId, int //hospitalId, string paciente, int? situacaoId

                string url = Constantes.URL_API + $"solicitar_contato";
                var req = new Request(url);
                var s = new SolicitarVM(){id=Session.UsuarioLogado.Id, id2 = id};
                return await req.Post<Object>(s);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public class AceitarVM
        {
            public int id { get; set; }
            public int idcontato { get; set; }
        }

        public static async Task<Object> AceitarContato(int id)
        {
            try
            {
                //string cnpj, string cpf, DateTime? mes, DateTime? inicioPeriodo, DateTime? fimPeriodo, int convenioId, int //hospitalId, string paciente, int? situacaoId

                string url = Constantes.URL_API + $"aprovar_contato";
                var req = new Request(url);
                var s = new AceitarVM() { id = Session.UsuarioLogado.Id, idcontato = id };
                return await req.Post<Object>(s);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public class InserirNotaVM
        {
            public int id { get; set; }
            public int id2 { get; set; }
            public int nota { get; set; }
        }

        public static async Task<Object> InserirNota(int id, int nota)
        {
            try
            {
                var i = new InserirNotaVM() {id = Session.UsuarioLogado.Id, id2 = id, nota = nota};

                string url = Constantes.URL_API + $"inserir_nota";
                var req = new Request(url);
                
                return await req.Post<Object>(i);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static async Task<Usuario> Info(int id)
        {
            try
            {
                //string cnpj, string cpf, DateTime? mes, DateTime? inicioPeriodo, DateTime? fimPeriodo, int convenioId, int //hospitalId, string paciente, int? situacaoId

                string url = Constantes.URL_API + $"usuario_info?id={id}";
                var req = new Request(url);
                return await req.Get<Usuario>();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static async Task<decimal?> Nota(int id)
        {
            try
            {
                //string cnpj, string cpf, DateTime? mes, DateTime? inicioPeriodo, DateTime? fimPeriodo, int convenioId, int //hospitalId, string paciente, int? situacaoId

                string url = Constantes.URL_API + $"usuario_nota?id={id}";
                var req = new Request(url);
                return await req.Get<decimal?>();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
