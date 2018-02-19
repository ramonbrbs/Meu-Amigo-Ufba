using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeuAmigo.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MeuAmigo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Cadastrar_Clicked(object sender, EventArgs e)
        {
            Session.Navigation.Navigation.PushAsync(new Cadastro());
        }

        private void BtnEntrar_OnClicked(object sender, EventArgs e)
        {
            
            Session.Navigation.Navigation.PushAsync(new Inicial());
            Session.Navigation.Navigation.RemovePage(this);
        }
    }
}