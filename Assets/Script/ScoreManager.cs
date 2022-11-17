using UnityEngine;
using UnityEngine.Events;
public class ScoreManager : MonoBehaviour
    {
        private int _score;
        [SerializeField] private UnityEvent<int> ScoreUpdate;
        [SerializeField] private AddCoin _addScore;

        private void OnEnable()
        {
            _addScore.AddListener(AddMyScore);
            Debug.Log("Add the coin score" +_addScore);
        }

        void AddMyScore(int increment)
        {
            _score += increment;
            ScoreUpdate?.Invoke(_score);
        }

        private void OnDisable()
        {
            _addScore.RemoveListener(AddMyScore);
        }
    }