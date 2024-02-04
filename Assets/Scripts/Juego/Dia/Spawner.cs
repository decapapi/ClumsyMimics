using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> enemigos = new List<GameObject>();

    public List<GameObject> objetos = new List<GameObject>();

    private GameObject jugador;
    void Start()
    {
        jugador = GameObject.Find("Jugador");
        int tipo = Random.Range(1, 3);
        int categoria = Random.Range(1, 5);

        if (tipo == 1)
        {
            if (categoria == 1)
            {
                GameObject enemigo = Instantiate(enemigos[categoria-1], transform.position, Quaternion.identity) as GameObject;
                enemigo.GetComponent<SeguimientoEnemigo>().jugador = jugador;
            } else if (categoria == 2)
            {
                GameObject enemigo = Instantiate(enemigos[categoria-1], transform.position, Quaternion.identity) as GameObject;
                enemigo.GetComponent<SeguimientoEnemigo>().jugador = jugador;
            } else if (categoria == 3)
            {
                GameObject enemigo = Instantiate(enemigos[categoria-1], transform.position, Quaternion.identity) as GameObject;
                enemigo.GetComponent<SeguimientoEnemigo>().jugador = jugador;
            } else if (categoria == 4)
            {
                GameObject enemigo = Instantiate(enemigos[categoria-1], transform.position, Quaternion.identity) as GameObject;
                enemigo.GetComponent<SeguimientoEnemigo>().jugador = jugador;
            }
        } else if (tipo == 2) {
            if (categoria == 1)
            {
                GameObject objeto = Instantiate(objetos[categoria-1], transform.position, Quaternion.identity) as GameObject;
            } else if (categoria == 2)
            {
                GameObject objeto = Instantiate(objetos[categoria-1], transform.position, Quaternion.identity) as GameObject;
            } else if (categoria == 3)
            {
                GameObject objeto = Instantiate(objetos[categoria-1], transform.position, Quaternion.identity) as GameObject;
            } else if (categoria == 4)
            {
                GameObject objeto = Instantiate(objetos[categoria-1], transform.position, Quaternion.identity) as GameObject;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
