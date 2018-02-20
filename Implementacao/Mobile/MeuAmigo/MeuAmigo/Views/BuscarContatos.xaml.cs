using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeuAmigo.Model;
using MeuAmigo.WS;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MeuAmigo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BuscarContatos : ContentPage
    {
        public BuscarContatos()
        {
            InitializeComponent();
        }

        public class BuscaVM
        {
            public Usuario Usuario { get; set; }
            public List<Lingua> Linguas { get; set; }

            public string LinguasStr => String.Join(",", Linguas);
        }

        protected override async void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                var usuarios = await UsuarioWS.GetBuscaContatos(Session.UsuarioLogado.Id);
                var lista = new List<BuscaVM>();
                foreach (var u in usuarios)
                {
                    var b = new BuscaVM();
                    b.Usuario = u;
                    b.Linguas = await UsuarioWS.GetLinguaUsuario(u.Id);
                    lista.Add(b);
                }
                Lista.ItemsSource = lista;
            }
            catch (Exception e)
            {
                
                throw;
            }
            
        }
    }
}