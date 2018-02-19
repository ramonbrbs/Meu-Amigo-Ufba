using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuAmigo.Model
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public int Idade { get; set; }
        public bool Tipo { get; set; }
        public string Senha { get; set; }
        public string Origem { get; set; }
    }
}
