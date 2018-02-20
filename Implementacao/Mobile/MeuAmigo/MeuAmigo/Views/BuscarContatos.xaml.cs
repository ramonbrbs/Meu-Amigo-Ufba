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
            public List<Interesse> Interesses { get; set; }
            public string LinguasStr => String.Join(",", Linguas);
            public string InteressesStr => String.Join(",", Interesses);
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
                    b.Interesses = await UsuarioWS.GetInteresseUsuario(u.Id);
                    lista.Add(b);
                }
                Lista.ItemsSource = lista;
                if (lista.Count == 0)
                {
                    Nofound.IsVisible = true;
                }
            }
            catch (Exception e)
            {
                
                throw;
            }
            
        }

        private async void Add_Tapped(object sender, EventArgs e)
        {
            try
            {
                ((Button)sender).IsVisible = false;
                int param = (int)((Button)sender).CommandParameter;
                await UsuarioWS.SolicitarContato(param);
            }
            catch (Exception exception)
            {
                
                throw;
            }
            
        }
    }
}