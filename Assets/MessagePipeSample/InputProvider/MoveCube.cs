using Cysharp.Threading.Tasks;
using MessagePipe;
using UniRx;
using UnityEngine;
using VContainer;

namespace MessagePipeSample.InputProvider
{
    public sealed class MoveCube : MonoBehaviour
    {
        /// <summary>
        /// MessagePipeからメッセージを受け取る用インタフェース
        /// </summary>
        [Inject] private ISubscriber<InputParams> _inputEventSubscriber;

        // 各種フィールド
        private CharacterController _characterController;
        private readonly float JumpSpeed = 3.0f;
        private readonly float MoveSpeed = 3.0f;

        private void Start()
        {
            _characterController = GetComponent<CharacterController>();
            
            // 入力イベントの受信を開始する
            _inputEventSubscriber.Subscribe(OnInputEventReceived)
                // MonoBehaviourに寿命を紐づける
                .AddTo(this.GetCancellationTokenOnDestroy());
        }

        /// <summary>
        /// 入力イベントを処理する
        /// </summary>
        private void OnInputEventReceived(InputParams input)
        {
            var moveVelocity = new Vector3(0, _characterController.velocity.y, 0);

            if (input.IsJump && _characterController.isGrounded)
            {
                moveVelocity += Vector3.up * JumpSpeed;
            }

            moveVelocity += input.Move * MoveSpeed;

            moveVelocity += Physics.gravity * Time.deltaTime;

            _characterController.Move(moveVelocity * Time.deltaTime);
        }
    }
}