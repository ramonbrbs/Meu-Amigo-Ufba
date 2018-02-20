using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeuAmigo.WS;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MeuAmigo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VisualizarPerfil : ContentPage
    {
        public VisualizarPerfil()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            var usuario = await UsuarioWS.Info();
            var vm = new BuscarContatos.BuscaVM();
            vm.Usuario = usuario;
            BindingContext = vm;
        }
    }
}