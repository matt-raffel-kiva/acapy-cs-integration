using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using connect.mediator.DTO;
using connect.mediator.Webhook;
using Microsoft.AspNetCore.Mvc;

namespace connect.mediator
{

    class Program
    {
        private static HttpClient client = new HttpClient();
        private static string ALICE = "localhost:8124";
        private static string FABER = "localhost:8224";
        public static WebHookModel FaberWebHookData = null;
        public static WebHookModel AliceWebHookData = null;

        private static async Task<string> GetResponse(HttpResponseMessage response, bool printResults = true)
        {
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            if (printResults)
            {
                Console.WriteLine(responseBody);
                Console.WriteLine("");
            }

            return responseBody;
        }

        private static async Task<T> MakeGetCall<T>(string host, string route, bool printResults = true)
        {
            Console.WriteLine($"GET : http://{host}/{route}");
            HttpResponseMessage response = await client.GetAsync($"http://{host}/{route}");
            response.EnsureSuccessStatusCode();
            string responseBody = await Program.GetResponse(response, printResults);

            T jsonObject = JsonConvert.DeserializeObject<T>(responseBody);

            return jsonObject;
        }

        private static async Task<T> MakePostCall<T, B>(string host, string route, B body, bool printResults = true)
        {
            Console.WriteLine($"POST : http://{host}/{route}");
            HttpResponseMessage response;
            if (body != null)
            {
                using (var content = new StringContent(JsonConvert.SerializeObject(body), System.Text.Encoding.UTF8, "application/json"))
                {
                    response = await client.PostAsync($"http://{host}/{route}", content);
                }
            }
            else
            {
                response = await client.PostAsync($"http://{host}/{route}", null);
            }

            string responseBody = await Program.GetResponse(response, printResults);

            T jsonObject = JsonConvert.DeserializeObject<T>(responseBody);

            return jsonObject;
        }

        private static void AddWebHookListenerEvent()
        {           
            WebHookBroadcaster.Instance.OnWebHookReceived += Broadcaster_OnWebHookReceived;
        }

        private static void RemoveWebHookListenerEvent()
        {
            WebHookBroadcaster.Instance.OnWebHookReceived -= Broadcaster_OnWebHookReceived;
        }

        private static void Broadcaster_OnWebHookReceived(string agentId, WebHookModel data)
        {
            Console.WriteLine($"webhook data recieved for {agentId}");
            if (agentId == "faber")
                Program.FaberWebHookData = data;
            if (agentId == "alice")
                Program.AliceWebHookData = data;
        }

        private static void PressAnyKey()
        {
            Console.WriteLine("\r\nPress any key...");
            Console.ReadKey();
            Console.WriteLine("");
        }

        private static async Task CreateConnection()
        {
            // Listing Faber connections
            Console.WriteLine("Listing Faber connections");
            await Program.MakeGetCall<ConnectionsResponse>(Program.FABER, "connections");


            // Listing Alice connections
            Console.WriteLine("Listing Alice connections");
            await Program.MakeGetCall<ConnectionsResponse>(Program.ALICE, "connections");

            Program.PressAnyKey();

            // 1.1  faber creates the invitation
            Console.WriteLine("1.1  faber creates the invitation");
            CreateInvitationResponse createInvitationResponse = await
                Program.MakePostCall<CreateInvitationResponse, object>(Program.FABER, "connections/create-invitation?auto_accept=false&alias=For-Meditaor", null);

            Program.PressAnyKey();

            // 2.1 pass the invitation on to alice
            Console.WriteLine("2.1 pass the invitation on to alice");
            ReceiveInvitationReply receiveInvitationReply = await
                Program.MakePostCall<ReceiveInvitationReply, Invitation>(Program.ALICE, "connections/receive-invitation?auto_accept=false&alias=For-Meditor", createInvitationResponse.invitation);

            Program.PressAnyKey();

            // 2.2 tell alice to accept invitation
            Console.WriteLine("2.2 tell alice to accept invitation");
            Program.AddWebHookListenerEvent();
            AcceptInvitationReply acceptInvitationReply = await
                Program.MakePostCall<AcceptInvitationReply, object>(Program.ALICE, $"connections/{receiveInvitationReply.connection_id}/accept-invitation", null);


            Program.PressAnyKey();

            // WE HAVE A PROBLEM HERE:
            // the connection ID needed is in faber. it is communicated through the webhook :/
            Console.WriteLine("3.1 faber completes the connection");
            object something = await
                Program.MakePostCall<object, object>(Program.FABER, $"connections/{Program.FaberWebHookData.ConnectionId}/accept-request", null);

            Program.PressAnyKey();
            Program.RemoveWebHookListenerEvent();

            // Listing Faber connections
            Console.WriteLine("Listing Faber connections");
            await Program.MakeGetCall<ConnectionsResponse>(Program.FABER, "connections");

            // Listing Alice connections
            Console.WriteLine("Listing Alice connections");
            await Program.MakeGetCall<ConnectionsResponse>(Program.ALICE, "connections");
        }

        static async Task Main(string[] args)
        {
            try
            {
                Console.WriteLine("Setting up webhook listener");
                _ = WebHostStartup.CreateHostBuilder(args).Build().RunAsync();
                Program.PressAnyKey();

                await Program.CreateConnection();

                // publish schema
                // http://localhost:8224/schemas

                // publish credential definition
                // http://localhost:8224/credential-definitions


            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }
    }
}


/*
 {"connection_id": "aa236c2e-55ac-422c-98e2-d86b02d35891", "invitation": {"@type": "did:sov:BzCbsNYhMrjHiqZDTUASHg;spec/connections/1.0/invitation", "@id": "aba4e269-e41e-48ea-b398-e5b47de86395", "label": "Faber.Agent", "serviceEndpoint": "http://192.168.65.3:8020", "recipientKeys": ["AVyCDUHJevAuJ1yRC2QEftGBw5ULMxjFCZpNERDvpPSK"]}, "invitation_url": "http://192.168.65.3:8020?c_i=eyJAdHlwZSI6ICJkaWQ6c292OkJ6Q2JzTlloTXJqSGlxWkRUVUFTSGc7c3BlYy9jb25uZWN0aW9ucy8xLjAvaW52aXRhdGlvbiIsICJAaWQiOiAiYWJhNGUyNjktZTQxZS00OGVhLWIzOTgtZTViNDdkZTg2Mzk1IiwgImxhYmVsIjogIkZhYmVyLkFnZW50IiwgInNlcnZpY2VFbmRwb2ludCI6ICJodHRwOi8vMTkyLjE2OC42NS4zOjgwMjAiLCAicmVjaXBpZW50S2V5cyI6IFsiQVZ5Q0RVSEpldkF1SjF5UkMyUUVmdEdCdzVVTE14akZDWnBORVJEdnBQU0siXX0=", "alias": "For-Meditaor"}
 */
