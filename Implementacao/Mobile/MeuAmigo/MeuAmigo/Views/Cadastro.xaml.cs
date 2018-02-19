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
    public partial class Cadastro : CarouselPage
    {
        public Cadastro()
        {
            InitializeComponent();
        }

        private void TxtNascimento_OnFocused(object sender, FocusEventArgs e)
        {
            TxtNascimento.Unfocus();
            Nascimento.Focus();
        }

        private void Nascimento_OnDateSelected(object sender, DateChangedEventArgs e)
        {
            TxtNascimento.Text = Nascimento.Date.ToString("dd/MM/yyyy");
        }

        private void Page1_Proximo(object sender, EventArgs e)
        {
            CurrentPage = PageSelecao;
        }

        private void Outside_Clicked(object sender, EventArgs e)
        {
            CurrentPage = LinguaCurso;
        }

        private void PageDetalhes_Proximo(object sender, EventArgs e)
        {
            CurrentPage = Interesses;
        }


        private void Concluir_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Concluído", "Obrigado por se cadastrar", "Ok");
            Session.Navigation.Navigation.PopToRootAsync(true);
        }
    }
}