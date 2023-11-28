using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class Autok
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("marka")]
        public string marka { get; set; }
        [JsonProperty("elerheto")]
        public bool elerheto { get; set; }
        [JsonProperty("azonosito")]
        public string azonosito {  get; set; }
    }
}
