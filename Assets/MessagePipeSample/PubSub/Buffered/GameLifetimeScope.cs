using MessagePipe;
using VContainer;
using VContainer.Unity;

namespace MessagePipeSample.PubSub.Buffered
{
    public class GameLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            // MessagePipeの設定
            var options = builder.RegisterMessagePipe();
            
            // KeyDataでグループ別けしてSendDataを送受信する
            builder.RegisterMessageBroker<SendData>(options);

            builder.RegisterEntryPoint<BufferSender>();
            builder.RegisterEntryPoint<BufferReceiver>();
        }
    }
}