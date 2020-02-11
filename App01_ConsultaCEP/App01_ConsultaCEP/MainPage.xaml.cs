using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using App01_ConsultaCEP.Servico;
using App01_ConsultaCEP.Servico.Modelo;

namespace App01_ConsultaCEP
{
    [DesignTimeVisible(false)]

    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Botao.Clicked += BuscarCEP;
        }

        private void BuscarCEP(object sender, EventArgs args)
        {
            //TODO - Lógica do Programa

            //TODO - Validações

            string cep = Cep.Text.Trim();

            if (isValidCep(cep))
            {
                try
                {
                    Endereco end = ViaCEPServico.BuscarEnderecoViaCEP(cep);

                    if(end != null)
                    {
                        Resultado.Text =
                        string.Format(
                            "Endereço:" +
                            "CEP: {0}, \n" +
                            "UF: {1}, \n" +
                            "Logradouro: {2}\n" +
                            "Bairro: {3}\n" +
                            "Localidade: {4}\n",
                            cep,
                            end.uf,
                            end.logradouro,
                            end.bairro,
                            end.localidade);
                    }
                    else
                    {
                        DisplayAlert("Erro!", "CEP Não encontrado: " + cep, "OK");
                    }
                }
                catch(Exception e)
                {
                    DisplayAlert("Erro Crítico!", e.Message, "OK");
                }
            } 
        }

        private bool isValidCep(string cep)
        {
            bool valido = true;
            if(cep.Length != 8)
            {
                //erro
                DisplayAlert("Erro!", "Cep Inválido! O Cep deve conter 8 caracteres", "ok");
                valido = false;
            }
            
            int NovoCEP = 0;

            if (!int.TryParse(cep, out NovoCEP))
            {
                //erro
                DisplayAlert("Erro!", "Cep Inválido! O Cep deve conter apenas números", "ok");
                valido = false;
            }

            return valido;
        }
    }
}
