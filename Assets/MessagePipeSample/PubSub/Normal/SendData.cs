using System;

namespace MessagePipeSample.PubSub.Normal
{
    /// <summary>
    /// 送信データ（適当に定義した型）
    /// </summary>
    public readonly struct SendData : IEquatable<SendData>
    {
        public int Id { get; }
        public int Value { get; }

        public SendData(int id, int value)
        {
            Value = value;
            Id = id;
        }
        
        public bool Equals(SendData other)
        {
            return Id == other.Id && Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            return obj is SendData other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Value);
        }
    }
}