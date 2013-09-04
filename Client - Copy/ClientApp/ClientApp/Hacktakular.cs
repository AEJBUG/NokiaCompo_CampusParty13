using System;
using System.Net.Browser;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.Text;

namespace ClientApp
{
    class Hacktakular
    {
        const string webURL = "http://ssh.angrykittens.co.uk:5000/api/{0}/";
        string apiKey;

        public Dictionary<string, List<Dictionary<string, string>>> Loads(string data)
        {
            return JsonConvert.DeserializeObject<Dictionary<string, List<Dictionary<string, string>>>>(data);

        }

        public async Task<String> MakeRequest(string url)
        {
            
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Method = "GET";

            HttpWebResponse response = (HttpWebResponse) await httpWebRequest.GetResponseAsync();
            Stream responseStream = response.GetResponseStream();
            string data;

            using (var reader = new System.IO.StreamReader(responseStream))
            {
                data = reader.ReadToEnd();
            }
            responseStream.Close();
            return data;
        }

        public async Task<Dictionary<string, List<Dictionary<string, string>>>> GetMenu(string id)
        {
            var data = await MakeRequest(String.Format(webURL, id));
            return Loads(data);            
        }

        public async void MakeOrder(List<int> order, String tag)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(String.Format(webURL, "order"));
            httpWebRequest.Method = "POST";
            httpWebRequest.Headers["tag"] = tag;
            byte[] data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(order));
            httpWebRequest.ContentLength = data.Length;
            var myStream = await httpWebRequest.GetRequestStreamAsync();
            myStream.Write(data, 0, data.Length);
            myStream.Close();
        }

        
    }
}
