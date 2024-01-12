using Newtonsoft.Json;
namespace DiverseMarket.UI.Util
{
    public class Endereco
    {
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Localidade { get; set; }
        public string Uf { get; set; }
    }

    public static class CepUtils
    {
        public static async Task<Endereco> GetAddressByCep(string cep)
        {
            using (var httpClient = new HttpClient())
            {
                string url = $"https://viacep.com.br/ws/{cep}/json/";
                var response = await httpClient.GetStringAsync(url);
                MessageBox.Show(response.ToString());
                return JsonConvert.DeserializeObject<Endereco>(response);
            }
        }
    }
}
