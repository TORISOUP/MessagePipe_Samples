using System;

namespace MessagePipeSample.PubSub.Key
{
    /// <summary>
    /// 識別子
    /// </summary>
    public readonly struct KeyData : IEquatable<KeyData>
    {
        public int _raw { get; }

        public KeyData(int raw)
        {
            _raw = raw;
        }

        public bool Equals(KeyData other)
        {
            return _raw == other._raw;
        }

        public override bool Equals(object obj)
        {
            return obj is KeyData other && Equals(other);
        }

        public override int GetHashCode()
        {
            return _raw;
        }
    }
}