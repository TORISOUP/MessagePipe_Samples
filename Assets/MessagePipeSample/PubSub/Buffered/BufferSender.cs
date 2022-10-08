using System;
using Cysharp.Threading.Tasks;
using MessagePipe;
using UnityEngine;
using VContainer.Unity;

namespace MessagePipeSample.PubSub.Buffered
{
    public sealed class BufferSender : IInitializable
    {
        private readonly IBufferedPublisher<SendData> _bufferedPublisher;

        public BufferSender(IBufferedPublisher<SendData> bufferedPublisher)
        {
            _bufferedPublisher = bufferedPublisher;
        }

        public void Initialize()
        {
            _bufferedPublisher.Publish(new SendData("1"));
            _bufferedPublisher.Publish(new SendData("2"));
            
            Debug.Log("Sender: Message sent.");
            
            UniTask.Void(async () =>
            {
                Debug.Log("Wait for a second...");
                await UniTask.Delay(TimeSpan.FromSeconds(1));
                _bufferedPublisher.Publish(new SendData("3"));
            });
        }
    }
}