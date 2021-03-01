using Cliente.Core.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Cliente.Core
{
    public partial class MainPage : ContentPage
    {
        DataServices _service;
        List<Aluno> alunos;

        public MainPage()
        {
            InitializeComponent();
            _service = new DataServices();
            ActualizarDados();
        }

        async void ActualizarDados()
        {
            alunos = await _service.TrazerAlunos();
            lvAlunos.ItemsSource = alunos.OrderBy(item => item.Nome).ToList();
        }

        private void lvAlunos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var aluno = e.SelectedItem as Aluno;
            entNome.Text = aluno.Nome;
            entSobreNome.Text = aluno.SobreNome;
        }

        private async void Update_clicked(object sender, EventArgs e)
        {
            if (Valida())
            {
                Aluno aluno = new Aluno
                {
                    Nome = entNome.Text.Trim(),
                    SobreNome = entSobreNome.Text.Trim(),
                };

                try
                {
                    await _service.ActualizarAlunos(aluno);
                    LimparCampos();
                    ActualizarDados();
                }
                catch (Exception)
                {
                    await DisplayAlert("Erro:", "", "OK");
                }
            }
            else
            {
                await DisplayAlert("Erro", "", "OK");
            }
        }

        private async void Delete_clicked(object sender, EventArgs e)
        {
            try
            {
                var m = ((MenuItem)sender);
                Aluno aluno = (Aluno)m.CommandParameter;
                await _service.ApagarAlunos(aluno);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private async void Adicionar_Clicked(object sender, EventArgs e)
        {
            if (Valida())
            {
                Aluno aluno = new Aluno
                {
                    Nome = entNome.Text.Trim(),
                    SobreNome = entSobreNome.Text.Trim(),
                };

                try
                {
                    await _service.AdicionarAlunos(aluno);
                    LimparCampos();
                    ActualizarDados();
                }
                catch (Exception)
                {
                    await DisplayAlert("Erro:", "", "OK");
                }
            }
            else
            {
                await DisplayAlert("Erro", "", "OK");
            }
        }

        private void LimparCampos()
        {
            entNome.Text = "";
            entSobreNome.Text = "";
        }

        private bool Valida()
        {
            if(string.IsNullOrEmpty(entNome.Text) && string.IsNullOrEmpty(entSobreNome.Text))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
