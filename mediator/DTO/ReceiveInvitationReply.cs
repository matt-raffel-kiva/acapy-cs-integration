using System;
namespace connect.mediator.DTO
{
    public class ReceiveInvitationReply
    {
        public string updated_at { get; set; }
        public string state { get; set; }
        public string their_label { get; set; }
        public string initiator { get; set; }
        public string connection_id { get; set; }
        public string invitation_mode { get; set; }
        public string my_did { get; set; }
        public string created_at { get; set; }
        public string accept { get; set; }
        public string request_id { get; set; }
        public string invitation_key { get; set; }
        public string routing_state { get; set; }
        public string alias { get; set; }

    }
}
