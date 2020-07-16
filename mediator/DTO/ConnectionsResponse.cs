using System.Collections.Generic;

namespace connect.mediator.DTO
{
    public class Connection
    {
        public string connection_id { get; set; }
        public string created_at { get; set; }
        public string invitation_mode { get; set; }
        public string initiator { get; set; }
        public string state { get; set; }
        public string invitation_key { get; set; }
        public string accept { get; set; }
        public string routing_state { get; set; }
        public string updated_at { get; set; }

    }

    public class ConnectionsResponse
    {
        public List<Connection> results { get; set; }

    }
}
