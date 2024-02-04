using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlJugador : MonoBehaviour
{
    public float velocidad = 50f;
    private Transform transform;
    public int vida = 5;
    private GameObject[] enemigos;
    public float cooldownDano = 1.5f;
    private bool puedeRecibirDano = true;
    public Rigidbody2D rb;
    public float knockback = 5f;
    public float knockbackDuration = 0.5f;
    private bool puedeDashear = true;
    private bool estaDasheando = false;
    public float dashCooldown = 1.5f;
    public float dashDuration = 0.5f;
    public float fuerzaDash = 10f;
    private float velocidadActual;
    private float speedTransitionTimer;
    private float tiempoEnTrigger = 0f;
    private ControlHUD controlHUD;
    Quaternion rotacion;

    public GameObject controlador;
    public ControlGlobal controlGlobalScript;
    private float transitionTime = 1f;
    public Animator transition;
    public ControlEscenas escenas;
    

    void Start()
    {
        controlHUD = GameObject.Find("HUD").GetComponent<ControlHUD>();
        transform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        escenas = GameObject.Find("GENERAL").GetComponent<ControlEscenas>();
        
        controlGlobalScript = FindObjectOfType<ControlGlobal>();
        if (controlGlobalScript != null)
        {
            vida = controlGlobalScript.Vidas;           
        }else{
            GameObject nuevoControl = Instantiate(controlador);
            controlGlobalScript = nuevoControl.GetComponent<ControlGlobal>();
        }
    }

    void Update()
    {
        Vector3 inputMovimiento = Vector3.zero;

        if (tiempoEnTrigger >= 3f)
        {
            escenas.CargarEscena("JuegoNoche");
        }

        if (Input.GetKey(KeyCode.W)) {
            inputMovimiento.y = 1;
            rotacion = Quaternion.Euler(0, 0, 90);
        }
        else if (Input.GetKey(KeyCode.S)) {
            inputMovimiento.y = -1;
            rotacion = Quaternion.Euler(0, 0, 270);
        }
        if (Input.GetKey(KeyCode.D)) {
            inputMovimiento.x = 1;
            rotacion = Quaternion.Euler(0, 0, 0);
        }
        else if (Input.GetKey(KeyCode.A)) {
            inputMovimiento.x = -1;
            rotacion = Quaternion.Euler(0, 0, 180);
        }
        if (Input.GetKeyDown(KeyCode.E) && puedeDashear)
        {
            StartCoroutine(Dash());
        }
        Mover(inputMovimiento);

        if (transform.position.x > -6f && transform.position.x < -2f && transform.position.y > -3f && transform.position.y < 2f)
        {
            tiempoEnTrigger += Time.deltaTime;
        }
        else
        {
            tiempoEnTrigger = 0f;
        }

        if (inputMovimiento == Vector3.zero && rotacion != null)
        {
            transform.rotation = rotacion;
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemigo"))
        {
            Vector2 diferencia = transform.position - other.transform.position;
            StartCoroutine(AplicarKnockback(diferencia));
            if (puedeRecibirDano)
            {
                RecibirDano();
                puedeRecibirDano = false;
                StartCoroutine(EsperarDano());
            }
        }
    }

    void Mover(Vector3 direccion)
    {
        if (!estaDasheando)
        {
            velocidadActual = velocidad;
        }
        else
        {
            speedTransitionTimer += Time.fixedDeltaTime;
            velocidadActual = Mathf.Lerp(velocidad, velocidad  * fuerzaDash, speedTransitionTimer / dashDuration);
            if (speedTransitionTimer >= dashDuration)
            {
                estaDasheando = false;
            }
        }
        //rb.velocity = direccion.normalized * velocidadActual;
        transform.position += direccion.normalized * Time.deltaTime * velocidadActual;
        float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * angulo);
    }

    private void RecibirDano()
    {
        vida--;
        controlHUD.QuitarCorazon();
        controlGlobalScript.Vidas = vida;
        if (vida <= 0)
        {
            controlGlobalScript.Resetear();
            escenas.CargarEscena("GameOver");
        }
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

    IEnumerator Dash()
    {
        puedeDashear = false;
        estaDasheando = true;
        speedTransitionTimer = 0f;
        yield return new WaitForSeconds(dashDuration);
        estaDasheando = false;
        yield return new WaitForSeconds(dashCooldown);
        puedeDashear = true;
    }
}
