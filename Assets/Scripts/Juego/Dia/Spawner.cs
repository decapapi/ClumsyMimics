using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> enemigos = new List<GameObject>();

    public List<GameObject> objetos = new List<GameObject>();

    private GameObject jugador;
    void Start()
    {
        jugador = GameObject.Find("Jugador");
        int tipo = Random.Range(1, 3);
        int categoria;

        if (tipo == 1)
        {
            categoria = Random.Range(1, 5);
            GameObject enemigo = Instantiate(enemigos[categoria-1], transform.position, Quaternion.identity) as GameObject;
            enemigo.GetComponent<SeguimientoEnemigo>().jugador = jugador;
        } else {
            categoria = Random.Range(1, 6);
            GameObject objeto = Instantiate(objetos[categoria-1], transform.position, Quaternion.identity) as GameObject;
        }
    }
}
