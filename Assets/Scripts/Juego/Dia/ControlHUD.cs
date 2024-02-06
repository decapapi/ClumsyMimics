using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlHUD : MonoBehaviour
{
    public Image[] corazones;
    public Image[] balas;

    private int corazonesRestante;
    private int balasRestantes;

    public GameObject controlador;
    public ControlGlobal controlGlobalScript;
    private AudioSource audioSource;

    void Start()
    {
        controlGlobalScript = FindObjectOfType<ControlGlobal>();
        audioSource = GetComponent<AudioSource>();
        if (controlGlobalScript != null)
        {
            corazonesRestante = controlGlobalScript.Vidas;
            balasRestantes = controlGlobalScript.Balas;
        }
        else
        {
            GameObject nuevoControl = Instantiate(controlador);
            controlGlobalScript = nuevoControl.GetComponent<ControlGlobal>();
            corazonesRestante = controlGlobalScript.Vidas;
            balasRestantes = controlGlobalScript.Balas;
        }

        for (int i = 0; i < corazones.Length; i++)
        {
            if (i < corazonesRestante)
            {
                corazones[i].enabled = true;
            }
            else
            {
                corazones[i].enabled = false;
            }
        }

        for (int i = 0; i < balas.Length; i++)
        {
            if (i < balasRestantes)
            {
                balas[i].enabled = true;
            }
            else
            {
                balas[i].enabled = false;
            }
        }
    }

    public void QuitarBala()
    {
        if (balasRestantes <= 0)
            return;
        balas[balasRestantes-1].enabled = false;
        balasRestantes--;
        controlGlobalScript.Balas = balasRestantes;
    }

    public void AnadirBala()
    {
        if (balasRestantes >= 5)
            return;
        balas[balasRestantes].enabled = true;
        balasRestantes++;
        var clip = Resources.Load("Sonido/Recoger objeto") as AudioClip;
        audioSource.PlayOneShot(clip);
        controlGlobalScript.Balas = balasRestantes;
    }

    public void QuitarCorazon()
    {
        if (corazonesRestante <= 0)
            return;
        
        corazones[corazonesRestante-1].enabled = false;
        corazonesRestante--;
        controlGlobalScript.Vidas = corazonesRestante;
    }
}
