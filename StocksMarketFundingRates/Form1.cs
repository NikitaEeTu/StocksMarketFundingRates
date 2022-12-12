using System.Diagnostics;
using System.Drawing.Text;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text.Json.Nodes;

namespace StocksMarketFundingRates
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string URL = "https://www.binance.com/fapi/v1/fundingRate";
            var Timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
            var coinSymbol = textBox2.Text;
            string urlParameters = string.Format("?symbol={0}&startTime={1}", coinSymbol, Timestamp);
            string urlWithParameters = URL + urlParameters;
           HttpClient client = new HttpClient();
            Debug.WriteLine(urlWithParameters);
            client.BaseAddress = new Uri(urlWithParameters);

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue(
                    "application/json")
                );

            HttpResponseMessage response = client.GetAsync(
                urlParameters)
                .Result;

            if (response.IsSuccessStatusCode) { 
                var dataObjects = response.Content.ReadAsStringAsync().Result;
                Debug.WriteLine(JsonArray.Parse(dataObjects));
            }
            else
            {
                Debug.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }

            client.Dispose();  
        }
    }
}