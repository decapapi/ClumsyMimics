using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlJugador : MonoBehaviour
{
    public float velocidad = 50f;
    private Transform transform;
    private bool MirandoDerecha = true;
    private bool MirandoArriba = true;
    void Start()
    {
        transform = GetComponent<Transform>();
    }

    void Update()
    {
        Vector3 inputMovimiento = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) {
            inputMovimiento.y = 1;
            if (!MirandoArriba)
                GirarY();
        }
        else if (Input.GetKey(KeyCode.S)) {
            inputMovimiento.y = -1;
            if (MirandoArriba)
                GirarY();
        }

        if (Input.GetKey(KeyCode.D)) {
            inputMovimiento.x = 1;
            if (!MirandoDerecha)
                GirarX();
        } else if (Input.GetKey(KeyCode.A)) {
            inputMovimiento.x = -1;
            if (MirandoDerecha)
                GirarX();
        }

        Mover(inputMovimiento);
    }

    void Mover(Vector3 direccion)
    {
        transform.position += direccion.normalized * Time.deltaTime * velocidad;
    }

    private void GirarX()
    {
        Vector3 rotacion;
        if (MirandoDerecha)
            rotacion = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
        else
            rotacion = new Vector3(transform.rotation.x, 0f, transform.rotation.z);

        transform.rotation = Quaternion.Euler(rotacion);
        MirandoDerecha = !MirandoDerecha;
    }

    private void GirarY()
    {
        Vector3 rotacion;
        if (MirandoArriba)
            rotacion = new Vector3(180f, transform.rotation.y, transform.rotation.z);
        else
            rotacion = new Vector3(0f, transform.rotation.y, transform.rotation.z);

        transform.rotation = Quaternion.Euler(rotacion);
        MirandoArriba = !MirandoArriba;
    }
}
