using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlHUD : MonoBehaviour
{
    public Image[] corazones;
    public Image[] balas;

    private int corazonesRestante;

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
        }
        else
        {
            GameObject nuevoControl = Instantiate(controlador);
            controlGlobalScript = nuevoControl.GetComponent<ControlGlobal>();
            corazonesRestante = controlGlobalScript.Vidas;
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
            if (i < controlGlobalScript.Balas)
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
        if (controlGlobalScript.Balas <= 0)
            return;
        balas[controlGlobalScript.Balas-1].enabled = false;
    }

    public void AnadirBala()
    {
        if (controlGlobalScript.Balas >= 5)
            return;
        balas[controlGlobalScript.Balas].enabled = true;
        var clip = Resources.Load("Sonido/Recoger objeto") as AudioClip;
        audioSource.PlayOneShot(clip);
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
