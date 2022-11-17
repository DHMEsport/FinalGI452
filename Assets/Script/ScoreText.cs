using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;

    public void UpdateScore(int currentScore)
    {
        _scoreText.text = $"Coins : {currentScore}";
        Debug.Log("Coin"+_scoreText);
    }
}