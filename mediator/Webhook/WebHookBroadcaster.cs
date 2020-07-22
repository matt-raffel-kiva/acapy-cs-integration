
namespace connect.mediator.Webhook
{
    public delegate void WebHookEvent(string agentId, WebHookModel data);
    
    public class WebHookBroadcaster
    {
        private static WebHookBroadcaster INSTANCE = new WebHookBroadcaster();
        private WebHookBroadcaster() { }

        public event WebHookEvent OnWebHookReceived;

        public void HaveReceivedWebHook(string agentId, WebHookModel data)
        {
            if (null == OnWebHookReceived) return;
            WebHookEvent eventHandler = OnWebHookReceived;

            eventHandler(agentId, data);
        }

        public static WebHookBroadcaster Instance
        {
            get
            {
                if (null == WebHookBroadcaster.INSTANCE)
                    WebHookBroadcaster.INSTANCE = new WebHookBroadcaster();

                return WebHookBroadcaster.INSTANCE;
            }
        }
    }
}
