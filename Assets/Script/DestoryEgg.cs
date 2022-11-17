using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DestoryEgg : MonoBehaviour
{
    [SerializeField] private GameObject Egg;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Egg"))
        {
            Destroy(Egg.gameObject);
        }
    }
}