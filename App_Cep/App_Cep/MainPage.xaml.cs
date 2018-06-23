using System;
using Xamarin.Forms;
using App_Cep.Servico;
using App_Cep.Servico.Modelo;
using System.ComponentModel;
using System.Threading.Tasks;

namespace App_Cep
{
    public partial class MainPage : ContentPage,INotifyPropertyChanged
    {

        public MainPage()
        {
            InitializeComponent();
            isLoading = false;
            //BOTAOCEP.Clicked += BuscarCep;
            BindingContext = this;
        }



        private bool isLoading;
        public bool IsLoading
        {
            get
            {
                return this.isLoading;
            }
            set
            {
                this.isLoading = value;
                RaisePropertyChanged("IsLoading");
            }
        }
#pragma warning disable CS0108 // O membro oculta o membro herdado; nova palavra-chave ausente
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore CS0108 // O membro oculta o membro herdado; nova palavra-chave ausente
        public void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        private async void BuscarCep(object sender, EventArgs e)
        {
            RESULTADO.Text = string.Empty;
            LodingTrue();
            await Task.Delay(4000);
            var Cep = CEP.Text;
            if (CepValido(Cep))
            {
                try
                {
                    var Endereco = ViaCepServiso.BuscarEnderecoPorCep(Cep);
                    if (Endereco != null)
                    {
                        RESULTADO.Text = string.Format("CEP: {0} Logradouro: {1} Complemento: {2}  Bairro: {3}  Localidade: {4} UF: {5}",
                            Endereco.Cep, Endereco.Logradouro, Endereco.Complemento, Endereco.Bairro, Endereco.Localidade, Endereco.Uf);
                    }
                    else
                    {
                        RESULTADO.Text = "Não foi possivel localizar o endereço para o CEP informado";
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Erro", ex.Message, "Ok");
                }
            }
            LodingFalse();
        }
        private bool CepValido(string cep)
        {

            var NovoCep = 0;
            if (string.IsNullOrEmpty(cep))
            {
                DisplayAlert("Erro", "O CEP inválido! Informe o Cep.", "Ok");
                return false;
            }
            if (cep.Length != 8)
            {
                DisplayAlert("Erro", "O CEP inválido! O Cep deve conter 8 caracteres.", "Ok");
                return false;
            }

            if (!int.TryParse(cep, out NovoCep))
            {
                DisplayAlert("Erro", "O CEP inválido! O Cep deve ser composto apenas por numero.", "Ok");
                return false;
            }
            return true;
        }
        public void LodingTrue()
        {
            IsLoading = true;
        }

        public void LodingFalse()
        {
            IsLoading = false;
        }
    }
}
