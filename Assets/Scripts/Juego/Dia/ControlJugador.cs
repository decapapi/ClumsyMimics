using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlJugador : MonoBehaviour
{
    public float velocidad = 50f;
    private Transform transform;
    void Start()
    {
        transform = GetComponent<Transform>();
    }

    void Update()
    {
        Vector3 inputMovimiento = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
            inputMovimiento.y = 1;
        else if (Input.GetKey(KeyCode.S))
                inputMovimiento.y = -1;

        if (Input.GetKey(KeyCode.D))
            inputMovimiento.x = 1;
        else if (Input.GetKey(KeyCode.A))
                inputMovimiento.x = -1;

        Mover(inputMovimiento);
    }

    void Mover(Vector3 direccion)
    {
        transform.position += direccion.normalized * Time.deltaTime * velocidad;
    }
}
