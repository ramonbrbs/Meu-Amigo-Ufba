using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeuAmigo.WS;
using Syncfusion.SfRating.XForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MeuAmigo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VisualizarPerfil : ContentPage
    {
        private int _id;
        public VisualizarPerfil(int id)
        {
            _id = id;
            InitializeComponent();
            
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            var usuario = await UsuarioWS.Info(_id);
            var vm = new BuscarContatos.BuscaVM();
            var interesses = await UsuarioWS.GetInteresseUsuario(_id);
            LstInteresses.ItemsSource = interesses;
            var linguas = await UsuarioWS.GetLinguaUsuario(_id);
            LstLinguas.ItemsSource = linguas;
            LblNota.Text = Convert.ToString(await UsuarioWS.Nota(_id));
            vm.Usuario = usuario;
            BindingContext = vm;
        }

        private async void Rating_OnValueChanged(object sender, ValueEventArgs e)
        {
            await UsuarioWS.InserirNota(_id, Convert.ToInt32(e.Value));
            DisplayAlert("Success", "Thanks for rating this user.", "Ok");
        }
    }
}