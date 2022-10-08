using System;
using MessagePipe;
using UnityEngine;
using VContainer.Unity;

namespace MessagePipeSample.PubSub.Buffered
{
    public sealed class BufferReceiver : IStartable, IDisposable
    {
        private readonly IBufferedSubscriber<SendData> _bufferedSubscriber;
        private IDisposable _disposable;

        public BufferReceiver(IBufferedSubscriber<SendData> bufferedSubscriber)
        {
            _bufferedSubscriber = bufferedSubscriber;
        }

        // Sender.Initialize()よりこっちの方が実行開始が遅い
        public void Start()
        {
            Debug.Log("BufferReceiver: Subscribe start.");
            _disposable = _bufferedSubscriber.Subscribe(data =>
            {
                Debug.Log($"Received! :{data.Value}");
            });
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}