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
                var contatos = await UsuarioWS.GetContatos(Session.UsuarioLogado.Id);
                var pendentes = await UsuarioWS.GetContatosPendentes(Session.UsuarioLogado.Id);
                LstContatos.ItemsSource = contatos;
                
                LstContatosPendentes.ItemsSource = pendentes;
                LstContatosPendentes.HeightRequest = 40;
                if (contatos.Count == 0)
                {
                    LblSemItens.IsVisible = true;
                }
            }
            catch (Exception e)
            {
                
                throw e;
            }
            
        }

        private void Search_clicked(object sender, EventArgs e)
        {
            Session.Navigation.Navigation.PushAsync(new BuscarContatos());
        }
    }
}