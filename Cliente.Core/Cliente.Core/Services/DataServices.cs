using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Cliente.Core.Services
{
    public class DataServices
    {
        HttpClient httpClient = new HttpClient();

        public async Task<List<Aluno>> TrazerAlunos()
        {
            try
            {
                string endpoint = "https://10.11.9.45:[port]/api/alunos";
                var resposta = await httpClient.GetStringAsync(endpoint);
                var alunos = JsonConvert.DeserializeObject<List<Aluno>>(resposta);
                return alunos; 
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task AdicionarAlunos(Aluno aluno)
        {
            try
            {
                string endpoint = "https://10.15.9.45:[port]/api/alunos/adicionar/{0}";
                var uri = new Uri(string.Format(endpoint, aluno.Id));
                var dados = JsonConvert.SerializeObject(aluno);
                var conteudo = new StringContent(dados, Encoding.UTF8, "application/json");

                HttpResponseMessage resposta = null;

                resposta = await httpClient.PostAsync(endpoint, conteudo);

                if (!resposta.IsSuccessStatusCode)
                {
                    throw new Exception("Falhar ao incluir o novo aluno, por favor consolte os logs.");
                }
            }
            catch (Exception)
            {
                 throw;
            }
        }

        public async Task ActualizarAlunos(Aluno aluno)
        {
            try
            {
                string endpoint = "https://10.15.9.45:[port]/api/alunos/actualizar/{0}";
                var uri = new Uri(string.Format(endpoint, aluno.Id));
                var dados = JsonConvert.SerializeObject(aluno);
                var conteudo = new StringContent(dados, Encoding.UTF8, "application/json");

                HttpResponseMessage resposta = null;

                resposta = await httpClient.PutAsync(endpoint, conteudo);

                if (!resposta.IsSuccessStatusCode)
                {
                    throw new Exception("Falhar ao actualizar  aluno, por favor consolte os logs.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task ApagarAlunos(Aluno aluno)
        {
            string endpoint = "https://10.15.9.45:[port]/api/alunos/delete/{0}";
            var uri = new Uri(string.Format(endpoint, aluno.Id));
            await httpClient.DeleteAsync(uri);
        }
    }
}
