using MessagePipe;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace MessagePipeSample.PubSub.Async
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private MovingCube _cubePrefab;

        protected override void Configure(IContainerBuilder builder)
        {
            // MessagePipeの設定
            var options = builder.RegisterMessagePipe();

            // デフォルトの非同期処理のやり方
            // PublishAsync時に個別指定もできる
            options.DefaultAsyncPublishStrategy = AsyncPublishStrategy.Parallel;

            builder.RegisterMessageBroker<TargetPosition>(options);
            builder.RegisterMessageBroker<ResetEvent>(options);

            // DIしながらInstantiate
            builder.RegisterBuildCallback(resolver =>
            {
                // 3つ生成する
                for (int i = 0; i < 3; i++)
                {
                    resolver.Instantiate(_cubePrefab);
                }
            });
        }
    }
}