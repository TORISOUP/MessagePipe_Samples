using MessagePipe;
using UnityEngine;
using VContainer.Unity;

namespace MessagePipeSample.InputProvider
{
    public sealed class InputEventProvider : ITickable
    {
        /// <summary>
        /// MessagePipeにメッセージを流す用のインタフェース
        /// </summary>
        private readonly IPublisher<InputParams> _inputPublisher;

        public InputEventProvider(IPublisher<InputParams> inputPublisher)
        {
            _inputPublisher = inputPublisher;
        }

        public void Tick()
        {
            // 入力状態を監視
            var isJump = Input.GetKey(KeyCode.Space);
            var axis = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            // メッセージを作成
            var inputParams = new InputParams(isJump, axis);

            // メッセージ送信
            _inputPublisher.Publish(inputParams);
        }
    }
}