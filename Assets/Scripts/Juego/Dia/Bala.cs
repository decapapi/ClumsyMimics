using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    public GameObject loot;
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemigo"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            Instantiate(loot, transform.position, Quaternion.identity);
        }
    }
}