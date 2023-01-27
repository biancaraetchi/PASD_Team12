using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Threading.Tasks;

class Program
 {
 // API endpoint URL
        static string apiUrl = "https://pasd-webshop-api.onrender.com/api/order/";
        // API key
        static string apiKey = "6MMRckGTM7HTwmV6CdEY";
static async Task RunAsync(){
        HttpClient client = new HttpClient();
        //client.BaseAddress = new Uri("http://localhost:64195/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("x-api-key", apiKey);
        var response = await client.GetAsync(apiUrl);
        if(response.IsSuccessStatusCode)
        {
            var jsonStringg =await response.Content.ReadAsStringAsync();
            Console.WriteLine(jsonStringg); //this writes the orders in JSON format in console.
            RootObject orders = JsonConvert.DeserializeObject<RootObject>(jsonStringg);

                // Sort the orders by send_date
                orders.orders = orders.orders.OrderBy(o => o.send_date).ToList();

                // Make an offer for each order an post it
                foreach (Order order in orders.orders)
                {   //Console.WriteLine(order.send_date.ToString("yyyy’-‘MM’-‘dd’T’HH’:’mm’:’ss.fffffffK"), order.x_in_mm, order.y_in_mm, order.z_in_mm, order.is_breakable, order.is_perishable,order.sender_info.name,order.sender_info.street_and_number, order.sender_info.zipcode,order.sender_info.city, order.sender_info.country);
                    Random rnd= new Random() ;
                    int Nprice = rnd.Next(0, 100);
                    Offer offer = new Offer
                    {

                        price_in_cents = Nprice,

                        expected_delivery_datetime = order.send_date.AddDays(7).AddHours(rnd.Next(10,19)).AddMinutes(rnd.Next(0,59)),
                        // actual_deliver_datetime= order.send_date.AddDays(7).AddHours(rnd.Next(10,19)).AddMinutes(rnd.Next(0,59)),
                        orderId = order.id,
                        // cost_in_cents = Nprice,
                        // status="EXP",
                        // id =order.id
                    };
                    //Console.WriteLine(offer);
                    //Console.WriteLine(order.id); // worked: we got the orders stored in the list with their proprieties.
                    string jsonOffer = JsonConvert.SerializeObject(offer);Console.WriteLine(jsonOffer);
                    StringContent content = new StringContent(jsonOffer, Encoding.UTF8, "application/json");//Console.WriteLine(content);
                    HttpResponseMessage reponse = await client.PostAsync("https://pasd-webshop-api.onrender.com/api/delivery/", content);
                    // if(reponse.IsSuccessStatusCode){
                    // Console.WriteLine("Response sucessful");
                    // HttpResponseMessage offerResponse = await client.PostAsJsonAsync( "https://pasd-webshop-api.onrender.com/api/delivery/", content);
                    // }
                    // else{
                    //     Console.WriteLine("Response not working");
                    // }
                }
        }
        else
        {
            Console.WriteLine("Error: " + response.ReasonPhrase);
        }
        //Get a delivery
            //var jsonString = await response.Content.ReadAsStringAsync();
       // Console.WriteLine(jsonString);

}
static void Main()
    {
        RunAsync().GetAwaiter().GetResult();
    }
}


class Offer{
        public int price_in_cents {get; set;}
        public DateTime expected_delivery_datetime {get; set;}
        //     public DateTime actual_deliver_datetime {get; set;}
         public int orderId {get; set;}
        //     public int cost_in_cents {get; set;}
        //     public string status {get; set;}
        //     public int id {get; set;}
        }
// RootObject class for deserializing JSON
class RootObject
{
    public List<Order> orders { get; set; }
}

//Order class
class Order
{
    public DateTime send_date { get; set; }
    public int x_in_mm { get; set; }
    public int y_in_mm { get; set; }
    public int z_in_mm { get; set; }
    public bool is_breakable { get; set; }
    public bool is_perishable { get; set; }
    public SenderInfo sender_info { get; set; }
    public ReceiverInfo receiver_info { get; set; }
    public int id { get; set; }
}

// SenderInfo and ReceiverInfo class
class SenderInfo
{
    public string name { get; set; }
    public string street_and_number { get; set; }
    public string zipcode { get; set; }
    public string city { get; set; }
    public string country { get; set; }
}
class ReceiverInfo
{
    public string name { get; set; }
    public string street_and_number { get; set; }
    public string zipcode { get; set; }
    public string city { get; set; }
    public string country { get; set; }
}
