using MessagePipe;
using VContainer;
using VContainer.Unity;

namespace MessagePipeSample.PubSub.Key
{
    public class GameLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            // MessagePipeの設定
            var options = builder.RegisterMessagePipe();
            
            // KeyDataでグループ別けしてSendDataを送受信する
            builder.RegisterMessageBroker<KeyData, SendData>(options);

            builder.RegisterEntryPoint<RoundRobinSender>();
            builder.RegisterEntryPoint<Receiver>();
        }
    }
}