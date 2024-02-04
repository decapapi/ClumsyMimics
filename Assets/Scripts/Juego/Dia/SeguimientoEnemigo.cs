using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeguimientoEnemigo : MonoBehaviour
{
    public GameObject jugador;
    public float velocidad = 10f;

    private float distancia;
    public float distanciaAtaque = 35f;
    public float rotacion = 0f; 
    public GameObject tipo;
    void Start()
    {
    }
    void Update()
    {
        distancia = Vector2.Distance(jugador.transform.position, transform.position);
        Vector2 direccion = (jugador.transform.position - transform.position).normalized;
        float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * angulo + new Vector3(0, 0, rotacion));
        if (distancia < distanciaAtaque)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, jugador.transform.position, velocidad * Time.deltaTime);
            transform.rotation = Quaternion.Euler(Vector3.forward * angulo + new Vector3(0, 0, rotacion));
        }
    }
}
