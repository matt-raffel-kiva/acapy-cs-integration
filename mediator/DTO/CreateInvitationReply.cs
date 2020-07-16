using System.Collections.Generic;

namespace connect.mediator.DTO
{
    public class Invitation
    {
        public string @type { get; set; }
        public string @id { get; set; }
        public string label { get; set; }
        public string serviceEndpoint { get; set; }
        public List<string> recipientKeys { get; set; }

    }

    public class CreateInvitationResponse
    {
        public string connection_id { get; set; }
        public Invitation invitation { get; set; }
        public string invitation_url { get; set; }
        public string alias { get; set; }

    }
}
