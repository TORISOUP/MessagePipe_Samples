using System;
using UnityEngine;

namespace MessagePipeSample.InputProvider
{
    /// <summary>
    /// ブロードキャストするメッセージ
    /// 入力情報
    /// </summary>
    public readonly struct InputParams : IEquatable<InputParams>
    {
        /// <summary>
        /// ジャンプフラグ
        /// </summary>
        public bool IsJump { get; }

        /// <summary>
        /// 移動操作
        /// </summary>
        public Vector3 Move { get; }
        
        public InputParams(bool isJump, Vector3 move)
        {
            IsJump = isJump;
            Move = move;
        }

        public bool Equals(InputParams other)
        {
            return IsJump == other.IsJump && Move.Equals(other.Move);
        }

        public override bool Equals(object obj)
        {
            return obj is InputParams other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IsJump, Move);
        }
    }
}