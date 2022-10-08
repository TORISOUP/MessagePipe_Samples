using MessagePipe;
using VContainer.Unity;

namespace MessagePipeSample.PubSub.Key
{
    /// <summary>
    /// 順繰りにデータを送る
    /// </summary>
    public sealed class RoundRobinSender : ITickable
    {
        private readonly IPublisher<KeyData, SendData> _publisher;

        private int _roundRobinIndex;

        public RoundRobinSender(IPublisher<KeyData, SendData> publisher)
        {
            _publisher = publisher;
        }

        public void Tick()
        {
            // 0,1,2 を順繰りにデータ送信する
            _roundRobinIndex = (_roundRobinIndex + 1) % 3;

            var key = new KeyData(_roundRobinIndex);
            var sendData = new SendData($"Key={_roundRobinIndex},Value={UnityEngine.Random.Range(0, 100)}");

            // Keyを指定して送信
            _publisher.Publish(key, sendData);
        }
    }
}