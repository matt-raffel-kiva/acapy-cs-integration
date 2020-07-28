using System.Collections.Generic;
using Newtonsoft.Json;

namespace connect.mediator.DTO
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Attribute
    {

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

    }

    public class CredentialProposal
    {

        [JsonProperty("@type")]
        public string Type { get; set; }

        [JsonProperty("attributes")]
        public List<Attribute> Attributes { get; set; }

    }

    public class IssueCredentialRequest
    {

        [JsonProperty("trace")]
        public bool Trace { get; set; }

        [JsonProperty("cred_def_id")]
        public string CredDefId { get; set; }

        [JsonProperty("credential_proposal")]
        public CredentialProposal CredentialProposal { get; set; }

        [JsonProperty("schema_name")]
        public string SchemaName { get; set; }

        [JsonProperty("schema_version")]
        public string SchemaVersion { get; set; }

        [JsonProperty("schema_id")]
        public string SchemaId { get; set; }

        [JsonProperty("auto_remove")]
        public bool AutoRemove { get; set; }

        [JsonProperty("issuer_did")]
        public string IssuerDid { get; set; }

        [JsonProperty("schema_issuer_did")]
        public string SchemaIssuerDid { get; set; }

        [JsonProperty("comment")]
        public string Comment { get; set; }

        [JsonProperty("connection_id")]
        public string ConnectionId { get; set; }

    }
}
