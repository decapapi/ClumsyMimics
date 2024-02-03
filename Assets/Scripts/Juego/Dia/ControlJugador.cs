using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlJugador : MonoBehaviour
{
    public float velocidad = 50f;
    private Transform transform;
    public float vida = 100f;
    private GameObject[] enemigos;
    public float cooldownDano = 1.5f;
    private bool puedeRecibirDano = true;
    public Rigidbody2D rb;
    public float knockback = 5f;
    public float knockbackDuration = 0.5f;

    void Start()
    {
        transform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector3 inputMovimiento = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) {
            inputMovimiento.y = 1;
        }
        else if (Input.GetKey(KeyCode.S)) {
            inputMovimiento.y = -1;
        }

        if (Input.GetKey(KeyCode.D)) {
            inputMovimiento.x = 1;
        }
        else if (Input.GetKey(KeyCode.A)) {
            inputMovimiento.x = -1;
        }

        Mover(inputMovimiento);

        Debug.Log("Vida: " + vida);
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemigo"))
        {
            Vector2 diferencia = transform.position - other.transform.position;
            StartCoroutine(AplicarKnockback(diferencia));
            if (puedeRecibirDano)
            {
                RecibirDano(10f);
                puedeRecibirDano = false;
                StartCoroutine(EsperarDano());
            }
        }
    }

    void Mover(Vector3 direccion)
    {
        transform.position += direccion.normalized * Time.deltaTime * velocidad;
        float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * angulo);
    }

    private void RecibirDano(float dano)
    {
        vida -= dano;
        if (vida <= 0)
            Destroy(gameObject);
    }

    IEnumerator EsperarDano()
    {
        yield return new WaitForSeconds(cooldownDano);
        puedeRecibirDano = true;
    }

    IEnumerator AplicarKnockback(Vector2 diferencia)
    {
        float elapsedTime = 0;
        Vector2 originalPosition = transform.position;
        Vector2 targetPosition = new Vector2(transform.position.x + diferencia.x * knockback, transform.position.y + diferencia.y * knockback);

        while (elapsedTime < knockbackDuration)
        {
            transform.position = Vector2.Lerp(originalPosition, targetPosition, elapsedTime / knockbackDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
    }
}
