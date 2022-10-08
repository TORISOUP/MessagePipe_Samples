using System;
using UnityEngine;

namespace MessagePipeSample.PubSub.Async
{
    /// <summary>
    /// 目的地情報
    /// </summary>
    public readonly struct TargetPosition : IEquatable<TargetPosition>
    {
        public Vector3 Position { get; }

        public TargetPosition(Vector3 position)
        {
            Position = position;
        }

        public bool Equals(TargetPosition other)
        {
            return Position.Equals(other.Position);
        }

        public override bool Equals(object obj)
        {
            return obj is TargetPosition other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Position.GetHashCode();
        }
    }
}