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
    public partial class Inicial : ContentPage
    {
        public Inicial()
        {
            InitializeComponent();
            
        }

        protected override async void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                await AtualizarListas();
            }
            catch (Exception e)
            {
                
                throw e;
            }
            
        }


        private async Task<bool> AtualizarListas()
        {
            var contatos = await UsuarioWS.GetContatos(Session.UsuarioLogado.Id);
            var pendentes = await UsuarioWS.GetContatosPendentes(Session.UsuarioLogado.Id);
            LstContatos.ItemsSource = contatos;

            //LstContatosPendentes.ItemsSource = pendentes;


            var lista = new List<BuscarContatos.BuscaVM>();
            foreach (var u in pendentes)
            {
                var b = new BuscarContatos.BuscaVM();
                b.Usuario = u;
                b.Linguas = await UsuarioWS.GetLinguaUsuario(u.Id);
                b.Interesses = await UsuarioWS.GetInteresseUsuario(u.Id);
                lista.Add(b);
            }
            LstContatosPendentes.ItemsSource = lista;
            if (lista.Count == 0)
            {
                LstContatosPendentes.IsVisible = false;
                LblPendentes.IsVisible = false;
            }
            else
            {
                LstContatosPendentes.IsVisible = true;
                LblPendentes.IsVisible = true;
            }
            if (contatos.Count == 0)
            {
                LblSemItens.IsVisible = true;
            }
            else
            {
                LblSemItens.IsVisible = false;
            }
            return true;
        }

        private void Search_clicked(object sender, EventArgs e)
        {
            Session.Navigation.Navigation.PushAsync(new BuscarContatos());
        }

        private async void Aceitar_Tapped(object sender, EventArgs e)
        {
            ((Button)sender).IsVisible = false;
            int param = (int)((Button)sender).CommandParameter;
            await UsuarioWS.AceitarContato(param);
            LstContatosPendentes.ItemsSource = null;
            LstContatos.ItemsSource = null;
            AtualizarListas();
        }

        private void LstContatos_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var u = (Usuario)e.Item;
            Session.Navigation.Navigation.PushAsync(new VisualizarPerfil(u.Id));
        }
    }
}