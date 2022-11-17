using UnityEngine;

[CreateAssetMenu(menuName = "Event Chanels/Add Score")]
public class AddCoin : ScriptableObject
{
    private event System.Action<int> _onAddScore = delegate { };

    public void RaiseEvent(int increment)
    {
        _onAddScore?.Invoke(increment);
        Debug.Log(increment);
    }

    public void AddListener(System.Action<int> callback)
    {
        _onAddScore += callback;
        Debug.Log(callback);
    }

    public void RemoveListener(System.Action<int> callback)
    {
        _onAddScore -= callback;
        Debug.Log(callback);
    }
}