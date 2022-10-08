using MessagePipe;
using VContainer;
using VContainer.Unity;

namespace MessagePipeSample.PubSub.Normal
{
    public class GameLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            // MessagePipeの設定
            var options = builder.RegisterMessagePipe();


            // SendDataを伝達できるように設定する
            builder.RegisterMessageBroker<int, SendData>(options);

            // ReadWriteSample1を起動
            builder.RegisterEntryPoint<ReadWriteSample1>(Lifetime.Singleton);
        }
    }
}