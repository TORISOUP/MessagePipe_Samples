using MessagePipe;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace MessagePipeSample.InputProvider
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private MoveCube _moveCubePrefab;

        protected override void Configure(IContainerBuilder builder)
        {
            // MessagePipeの設定
            var options = builder.RegisterMessagePipe();

            // InputParamsを伝達できるように設定する
            builder.RegisterMessageBroker<InputParams>(options);

            // InputEventProviderを起動
            builder.RegisterEntryPoint<InputEventProvider>(Lifetime.Singleton);

            // MoveCubeをDIしながらInstantiate
            builder.RegisterBuildCallback(resolver =>
            {
                // --------------------
                
                // 3つ並べて生成する
                for (int i = 0; i < 3; i++)
                {
                    var cube = resolver.Instantiate(_moveCubePrefab);
                    cube.transform.position = Vector3.forward * (i * 2f);
                }
                
                // -------------------
            });
        }
    }
}