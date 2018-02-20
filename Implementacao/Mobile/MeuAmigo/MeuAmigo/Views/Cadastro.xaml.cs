using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeuAmigo.Model;
using MeuAmigo.WS;
using Plugin.Media;
using Plugin.Media.Abstractions;
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

        private byte[] foto;
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            PckCurso.ItemsSource = await ListagensCadastroWS.GetCurso();
            PckLinguas.ItemsSource = await ListagensCadastroWS.GetLinguas();
            LstInteresses.ItemsSource = await ListagensCadastroWS.GetInteresses();
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

        private int tipo;

        private void Outside_Clicked(object sender, EventArgs e)
        {
            tipo = 1;
            CurrentPage = LinguaCurso;
        }

        private void PageDetalhes_Proximo(object sender, EventArgs e)
        {
            CurrentPage = Interesses;
        }


        private async void Concluir_Clicked(object sender, EventArgs e)
        {
            var u = new Usuario();
            u.Email = TxtEmail.Text;
            u.Nascimento = Nascimento.Date;
            u.Nome = TxtNome.Text;
            u.Origem = TxtOrigem.Text;
            u.Senha = TxtSenha.Text;
            u.Telefone = TxtTelefone.Text;
            u.Curso_Id = ((Curso) PckCurso.SelectedItem).Id;
            u.Foto = foto;
            u.Tipo = tipo.ToString();
            var retorno = await UsuarioWS.Cadastrar(u);
            if (retorno != null)
            {
                var linguas = (List<Lingua>) LstLinguas.ItemsSource;
                var interesses = new List<Interesse>();
                foreach (var i in LstInteresses.SelectedItems)
                {
                    var ui = new UsuarioInteresse() {Id_Interesse = ((Interesse) i).Id, Id_Usuario = retorno.Id};
                    await UsuarioWS.CadastrarInteresses(ui);
                }

                foreach (var l in (List<Lingua>)LstLinguas.ItemsSource)
                {
                    var ul = new UsuarioLingua(){Id_Lingua = l.Id, Id_Usuario = retorno.Id};
                    await UsuarioWS.CadastrarLinguas(ul);
                }

                DisplayAlert("Concluído", "Obrigado por se cadastrar", "Ok");
                Session.Navigation.Navigation.PopToRootAsync(true);
            }
            else
            {
                DisplayAlert("Erro", "Erro ao cadastrar", "Ok");
            }
            
        }

        private void PckLinguas_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (PckLinguas.SelectedIndex == -1)
                return;
            var itens = (List<Lingua>) LstLinguas.ItemsSource;
            if (itens == null)
            {
                itens = new List<Lingua>();
            }
            LstLinguas.ItemsSource = null;
            itens.Add((Lingua)PckLinguas.SelectedItem);
            LstLinguas.ItemsSource = itens;
            PckLinguas.SelectedIndex = -1;
        }

        private async void PhotoAdd_Clicked(object sender, EventArgs e)
        {
            try
            {
                await CrossMedia.Current.Initialize();
                
                var file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions());

                if (file == null)
                    return;

                await DisplayAlert("File Location", file.Path, "OK");
                MemoryStream ms = new MemoryStream();

                var str = file.GetStream();
                var fotoSource = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    file.Dispose();
                    str = stream;
                    return stream;
                });

                str.CopyTo(ms);
                foto = ms.ToArray();
                ImgFoto.Source = fotoSource;
                ImgFoto.IsVisible = true;
            }
            catch (Exception exception)
            {
                
                throw exception;
            }
            
        }

        private void Ufba_Clicked(object sender, EventArgs e)
        {
            tipo = 0;
            CurrentPage = LinguaCurso;
        }
    }
}