using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparar : MonoBehaviour
{
    public Transform firePoint;
    public GameObject balaPrefab;
    public float fuerzaBalas = 20f;
    private bool puedeDisparar = true;
    private ControlDia controlDia;
    private AudioSource audioSource;

    void Start()
    {
        controlDia = GameObject.Find("EventSystem").GetComponent<ControlDia>();
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && controlDia.RecuentoBalas() > 0 && puedeDisparar)
        {
            Disparo();
            controlDia.RestarBalas();
            StartCoroutine(EsperarDisparo());
        }
    }
    void Disparo()
    {
        GameObject bala = Instantiate(balaPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bala.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * fuerzaBalas, ForceMode2D.Impulse);
        puedeDisparar = false;
        StartCoroutine(SonidoDisparo());
    }
    IEnumerator EsperarDisparo()
    {
        yield return new WaitForSeconds(1f);
        puedeDisparar = true;
    }

    IEnumerator SonidoDisparo()
    {
        var clip = Resources.Load("Sonido/Disparo") as AudioClip;
        audioSource.PlayOneShot(clip);
        yield return new WaitForSeconds(0.5f);
        clip = Resources.Load("Sonido/Recarga rifle 1") as AudioClip;
        audioSource.PlayOneShot(clip);
        yield return new WaitForSeconds(0.5f);
        clip = Resources.Load("Sonido/Bala en el suelo") as AudioClip;
        audioSource.PlayOneShot(clip);
    }
}