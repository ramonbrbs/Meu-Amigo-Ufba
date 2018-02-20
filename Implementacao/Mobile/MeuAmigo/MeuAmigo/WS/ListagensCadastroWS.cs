using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeuAmigo.Model;

namespace MeuAmigo.WS
{
    public class ListagensCadastroWS
    {
        public static async Task<List<Lingua>> GetLinguas()
        {
            try
            {
                //string cnpj, string cpf, DateTime? mes, DateTime? inicioPeriodo, DateTime? fimPeriodo, int convenioId, int //hospitalId, string paciente, int? situacaoId

                string url = Constantes.URL_API + "linguas";
                var req = new Request(url);
                return await req.Get<List<Lingua>>();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static async Task<List<Curso>> GetCurso()
        {
            try
            {
                //string cnpj, string cpf, DateTime? mes, DateTime? inicioPeriodo, DateTime? fimPeriodo, int convenioId, int //hospitalId, string paciente, int? situacaoId

                string url = Constantes.URL_API + "cursos";
                var req = new Request(url);
                return await req.Get<List<Curso>>();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static async Task<List<Interesse>> GetInteresses()
        {
            try
            {
                //string cnpj, string cpf, DateTime? mes, DateTime? inicioPeriodo, DateTime? fimPeriodo, int convenioId, int //hospitalId, string paciente, int? situacaoId

                string url = Constantes.URL_API + "interesses";
                var req = new Request(url);
                return await req.Get<List<Interesse>>();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
