using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WPFApplication.View;
using System.Numerics;

namespace WPFApplication.Services
{
    public struct FactoryAcc
    {
       public void CenterWindowOnScreen(Window window)
       {
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;
            double windowWidth = window.Width;
            double windowHeight = window.Height;

            window.Left = (screenWidth - windowWidth) / 2;
            window.Top = (screenHeight - windowHeight) / 2;
       }
        public async Task ShowWait(int count)
        {
            GlobalServices.waitWpp.Show();

            GlobalServices.faseRequest = 1;

            Label lbl = GlobalServices.waitWpp.FindName("lblLoading") as Label;

            GlobalServices.AppendTextWaitWindow($"Fazendo requizição de {count} cadastros!", lbl);
            
            await Task.Delay(1500);
            
            List<GerarPessoaResponse> pessoas = await FazerRequisicaoGet(count);

            int i = 0;
            GlobalServices.faseRequest = 2;

            foreach (var pessoa in pessoas)
            {
                i++;
                float _value = ((float)i / pessoas.Count);
                string _porcentagem = $"{_value * 100}%";
                string _valorFinal = String.Format(CultureInfo.InvariantCulture, "{0}", _porcentagem);

                GlobalServices.AppendTextWaitWindow($"Carregando...\nbaixando:{pessoas.Count} pessoas!\nNome: {pessoa.Nome}\nIdade: {pessoa.Idade}\n ( {i} de {pessoas.Count} )\nConcluído: {_valorFinal}", lbl);
                await Task.Delay(25);
            }           

            GlobalServices.waitWpp.Close();
        }

        static async Task<List<GerarPessoaResponse>> FazerRequisicaoGet(int count)
        {
            string url = $"https://localhost:44373/api/ExternAccess/gerar?quantidade={count}";

            using (HttpClient httpClient = new HttpClient())
            {
                HttpResponseMessage response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string conteudo = await response.Content.ReadAsStringAsync();
                    List<GerarPessoaResponse> pessoas = JsonConvert.DeserializeObject<List<GerarPessoaResponse>>(conteudo);
                    return pessoas;
                }
                else
                {
                    throw new HttpRequestException($"Erro na requisição: {response.StatusCode}");
                }
            }
        }
    }
    public class GerarPessoaResponse
    {
        public string Nome { get; set; }
        public int Idade { get; set; }
    }
}
