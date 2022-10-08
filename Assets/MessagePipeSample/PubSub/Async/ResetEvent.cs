namespace MessagePipeSample.PubSub.Async
{
    // 状態リセットイベント
    public readonly struct ResetEvent
    {
        public static readonly ResetEvent Default = new ResetEvent();
    }
}