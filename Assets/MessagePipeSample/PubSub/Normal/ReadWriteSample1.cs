using System;
using MessagePipe;
using UnityEngine;
using VContainer.Unity;

namespace MessagePipeSample.PubSub.Normal
{
    public class ReadWriteSample1 : IInitializable, IDisposable, ITickable
    {
        private readonly IPublisher<SendData> _publisher;
        private readonly ISubscriber<SendData> _subscriber;
        private IDisposable _disposable;

        // それぞれをDIしてもらう
        public ReadWriteSample1(
            IPublisher<SendData> publisher,
            ISubscriber<SendData> subscriber)
        {
            _publisher = publisher;
            _subscriber = subscriber;
        }

        public void Initialize()
        {
            // 同じクラス内でPub/Subするのはナンセンスだけど、
            // 使い方のサンプルということでゆるして

            // SendDataを購読し、受信したらログにだす
            _disposable = _subscriber.Subscribe(data =>
            {
                // 受信データのうち、Idが一致するもののみ処理する
                if (data.Id == 0)
                {
                    Debug.Log($"Received: Id={data.Id} Value={data.Value}");
                }
            });
        }

        public void Tick()
        {
            // 毎フレーム送信する
            _publisher.Publish(new SendData(id: 0, UnityEngine.Random.Range(0, 100)));
        }

        public void Dispose()
        {
            // 購読中止
            _disposable?.Dispose();
        }
    }
}