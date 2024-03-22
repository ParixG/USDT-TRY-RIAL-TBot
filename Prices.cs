using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace tBot
{
    internal class Prices
    {
        public static async Task<double> RialMain()
        {
            string apiUrl = "https://api.nobitex.ir/v2/orderbook/USDTIRT";
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonContent = await response.Content.ReadAsStringAsync();

                        JObject data = JObject.Parse(jsonContent);

                        double lastValue = (double)data["lastTradePrice"]/10;

                        //Console.WriteLine($"USDT/IRL: {lastValue}");
                        return lastValue;
                    }
                    else
                    {
                        Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                        return 0.0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return 0.0;
            }
        }
        public static async Task<double> TryMain()
        {
            string apiUrl = "https://api.btcturk.com/api/v2/ticker?pairSymbol=USDTTRY";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonContent = await response.Content.ReadAsStringAsync();

                        JObject data = JObject.Parse(jsonContent);

                        double lastValue = (double)data["data"][0]["last"];

                        //Console.WriteLine($"USDT/TRY: {lastValue}");
                        return lastValue;
                    }
                    else
                    {
                        Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                        return 0.0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return 0.0;
            }
        }
    }
}

