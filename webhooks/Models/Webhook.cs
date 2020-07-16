using Newtonsoft.Json;

namespace connect.webhooks.Models
{
    public class Webhook
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        [JsonProperty("accept")]
        public string Accept { get; set; }

        [JsonProperty("alias")]
        public string Alias { get; set; }

        [JsonProperty("connection_id")]
        public string ConnectionId { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("initiator")]
        public string Initiator { get; set; }

        [JsonProperty("invitation_key")]
        public string InvitationKey { get; set; }

        [JsonProperty("invitation_mode")]
        public string InvitationMode { get; set; }

        [JsonProperty("my_did")]
        public string MyDid { get; set; }

        [JsonProperty("routing_state")]
        public string RoutingState { get; set; }

        [JsonProperty("request_id")]
        public string RequestId { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("their_did")]
        public string TheirDid { get; set; }

        [JsonProperty("their_label")]
        public string TheirLabel { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }


    }
}
