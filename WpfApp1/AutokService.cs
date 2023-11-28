using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace WpfApp1
{
    internal class AutokService
    {
        private HttpClient client = new HttpClient();
        private string url = "https://retoolapi.dev/O1jDPM/Autok";

        public List<Autok> GetAll()
        {
            string json = client.GetStringAsync(url).Result;
            return JsonConvert.DeserializeObject<List<Autok>>(json);
        }

        public Autok Add(Autok auto)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(auto), Encoding.UTF8, "application.json");
            HttpResponseMessage responseMessage = client.PostAsync(url, content).Result;
            string responseContent = responseMessage.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<Autok>(responseContent);
        }

        public bool Delete(Autok auto)
        {
            int id = auto.Id;
            HttpResponseMessage response = client.DeleteAsync($"{url}/{id}").Result;
            return response.IsSuccessStatusCode;
        }

        public Autok Update(int id, Autok auto)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(auto), Encoding.UTF8, "application.json");
            HttpResponseMessage responseMessage = client.PostAsync($"{url}/{id}", content).Result;

            string responseContent = responseMessage.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<Autok>(responseContent);
        }
    }
}
