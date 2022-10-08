using System.Threading;
using Cysharp.Threading.Tasks;
using MessagePipe;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using Random = UnityEngine.Random;

namespace MessagePipeSample.PubSub.Async
{
    /// <summary>
    /// uGUIに連動してCubeに命令を出す
    /// </summary>
    public sealed class CubeController : MonoBehaviour
    {
        [Inject] private IAsyncPublisher<TargetPosition> _asyncPublisher;
        [Inject] private IPublisher<ResetEvent> _resetPublisher;

        // 各種ボタン
        [SerializeField] private Button _moveParallelButton;
        [SerializeField] private Button _moveSequentialButton;
        [SerializeField] private Button _resetButton;

        [SerializeField] private Transform _goalMarkerObject;

        private void Start()
        {
            var ct = this.GetCancellationTokenOnDestroy();
            
            // ボタンイベントの購読開始
            WaitForResetButtonAsync(ct).Forget();
            WaitForMoveParallelButtonAsync(ct).Forget();
            WaitForMoveSequentialButtonAsync(ct).Forget();
        }

        /// <summary>
        /// リセットボタンの処理
        /// </summary>
        private async UniTaskVoid WaitForResetButtonAsync(CancellationToken ct)
        {
            var asyncHandler = _resetButton.GetAsyncClickEventHandler(ct);

            while (!ct.IsCancellationRequested)
            {
                // ボタンがクリックされたらリセットメッセージを発行
                await asyncHandler.OnClickAsync();
                _resetPublisher.Publish(ResetEvent.Default);
            }
        }

        /// <summary>
        /// 移動ボタン（並行処理）
        /// </summary>
        private async UniTaskVoid WaitForMoveParallelButtonAsync(CancellationToken ct)
        {
            var asyncHandler = _moveParallelButton.GetAsyncClickEventHandler(ct);

            while (!ct.IsCancellationRequested)
            {
                await asyncHandler.OnClickAsync();

                // PublishAsyncが完了するまでボタンを無効化
                SwitchButtonInteractable(false);
                
                // ランダムに移動先を決定
                var target = new UnityEngine.Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f));
                _goalMarkerObject.transform.position = target;

                // Cubeにメッセージを送信し、移動が終わるまで待つ
                // AsyncPublishStrategy.Parallel を指定しているので並行実行
                await _asyncPublisher.PublishAsync(new TargetPosition(target),
                    AsyncPublishStrategy.Parallel, ct);

                // ボタンを再有効化
                SwitchButtonInteractable(true);
            }
        }

        /// <summary>
        /// 移動ボタン（直列処理）
        /// </summary>
        private async UniTaskVoid WaitForMoveSequentialButtonAsync(CancellationToken ct)
        {
            var asyncHandler = _moveSequentialButton.GetAsyncClickEventHandler(ct);

            while (!ct.IsCancellationRequested)
            {
                await asyncHandler.OnClickAsync();

                // PublishAsyncが完了するまでボタンを無効化
                SwitchButtonInteractable(false);

                // ランダムに移動先を決定
                var target = new UnityEngine.Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f));
                _goalMarkerObject.transform.position = target;

                // Cubeにメッセージを送信し、移動が終わるまで待つ
                // AsyncPublishStrategy.Sequential を指定しているので直列実行
                await _asyncPublisher.PublishAsync(new TargetPosition(target),
                    AsyncPublishStrategy.Sequential, ct);
                
                // ボタンを再有効化
                SwitchButtonInteractable(true);
            }
        }

        private void SwitchButtonInteractable(bool isEnabled)
        {
            _moveParallelButton.interactable = isEnabled;
            _moveSequentialButton.interactable = isEnabled;
            _resetButton.interactable = isEnabled;
        }
    }
}