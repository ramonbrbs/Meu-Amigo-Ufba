using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace MeuAmigo.Model
{
    public class Usuario
    {
        public int Id { get; set; }
        public byte[] Foto { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public DateTime Nascimento { get; set; }
        public string Tipo { get; set; }
        public string Senha { get; set; }
        public string Origem { get; set; }
        public int Curso_Id { get; set; }


        [JsonIgnore]
        public int IgnoreOnSerializing
        {
            get { return IdContato.Value; }
            set { this.IdContato = value; }
        }

        [JsonProperty(nameof(IdContato))]
        public int IgnoreOnSerializingSetter { set { IdContato = value; } }

        [JsonIgnore]
        public ImageSource FotoSource {
            get
            {
                return ImageSource.FromStream(() => new MemoryStream(Foto));
            } }

        [JsonIgnore]
        public int? IdContato { get; set; }

        public override string ToString()
        {
            return Nome;
        }




    }
}
