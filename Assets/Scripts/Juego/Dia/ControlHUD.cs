using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlHUD : MonoBehaviour
{
    public Image[] corazones;
    public Image[] balas;

    private int corazonesRestante = 5;
    private int balasRestantes = 5;

    public void QuitarBala()
    {
        if (balasRestantes <= 0)
            return;
        
        balas[balasRestantes-1].enabled = false;
        balasRestantes--;
    }

    public void QuitarCorazon()
    {
        if (corazonesRestante <= 0)
            return;
        
        corazones[corazonesRestante-1].enabled = false;
        corazonesRestante--;
    }
}
