using System;

public class MiscEvents
{
    public event Action onCoinCollected;

    public event Action onGemCollected;
    public void CoinCollected() => onCoinCollected?.Invoke();
    public void GemCollected() => onGemCollected?.Invoke();

}
