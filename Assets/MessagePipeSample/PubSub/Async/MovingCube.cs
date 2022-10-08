using System.Threading;
using Cysharp.Threading.Tasks;
using MessagePipe;
using UnityEngine;
using VContainer;

namespace MessagePipeSample.PubSub.Async
{
    public sealed class MovingCube : MonoBehaviour
    {
        [Inject] private IAsyncSubscriber<TargetPosition> _asyncTargetSubscriber;
        [Inject] private ISubscriber<ResetEvent> _resetEventSubscriber;

        private readonly float _speed = 5.0f;

        private void Start()
        {
            _asyncTargetSubscriber.Subscribe(async (target, ct) =>
                {
                    await MoveToTargetAsync(target.Position, ct);
                })
                .AddTo(this.GetCancellationTokenOnDestroy());

            _resetEventSubscriber.Subscribe(_ => ResetPosition())
                .AddTo(this.GetCancellationTokenOnDestroy());
        }

        /// <summary>
        /// 指定座標に向かってゆっくり移動する
        /// </summary>
        private async UniTask MoveToTargetAsync(Vector3 target, CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                // 目標地点との差分
                var delta = (target - transform.position);

                // 1.0m以内なら到着
                if (delta.sqrMagnitude < 1.0f)
                {
                    return;
                }

                // 離れているなら目標に向かって移動する
                transform.position += _speed * (delta.normalized) * Time.deltaTime;

                await UniTask.Yield();
            }
        }

        private void ResetPosition()
        {
            transform.position =
                new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f));
        }
    }
}