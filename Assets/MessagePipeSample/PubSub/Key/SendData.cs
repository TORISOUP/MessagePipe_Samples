using System;

namespace MessagePipeSample.PubSub.Key
{
    /// <summary>
    /// 送信データ（適当に定義した型）
    /// </summary>
    public readonly struct SendData : IEquatable<SendData>
    {
        public string Value { get; }

        public SendData(string value)
        {
            Value = value;
        }

        public bool Equals(SendData other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            return obj is SendData other && Equals(other);
        }

        public override int GetHashCode()
        {
            return (Value != null ? Value.GetHashCode() : 0);
        }
    }
}