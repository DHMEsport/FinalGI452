using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int _ScoreAmount = 1;
    [SerializeField] private AddCoin _eventchannel;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            _eventchannel.RaiseEvent(_ScoreAmount);
            Debug.Log("Coin is add and destroy the Coin");
            Destroy(gameObject);
        }
    }
}
