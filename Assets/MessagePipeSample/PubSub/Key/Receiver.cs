using System;
using MessagePipe;
using UnityEngine;
using VContainer.Unity;

namespace MessagePipeSample.PubSub.Key
{
    public sealed class Receiver : IInitializable, IDisposable
    {
        private readonly ISubscriber<KeyData, SendData> _subscriber;
        private IDisposable _disposable;

        public Receiver(ISubscriber<KeyData, SendData> subscriber)
        {
            _subscriber = subscriber;
        }

        public void Initialize()
        {
            // まとめて破棄するやつ
            var bag = DisposableBag.CreateBuilder();

            // --- Key = 0 のみを購読 ---
            _subscriber.Subscribe(new KeyData(0), data =>
                {
                    Debug.Log($"MyKey=0, ReceivedData=[{data.Value}]");
                })
            .AddTo(bag);

            // --- Key = 1 のみを購読 ---
            _subscriber.Subscribe(new KeyData(1), data =>
                {
                    Debug.Log($"MyKey=1, ReceivedData=[{data.Value}]");
                })
            .AddTo(bag);

            // --- Key = 2 は購読しない ---

            _disposable = bag.Build();
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}