using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlNPC : MonoBehaviour
{
    public Sprite[] imagenes;
    private Image imagen;
    void Start()
    {
        if (Screen.fullScreen == false)
        {
            imagen = GetComponent<Image>();
            imagen.sprite = imagenes[Random.Range(0, imagenes.Length)];
        }
        else
        {
            imagen = GetComponent<Image>();
            imagen.sprite = imagenes[Random.Range(0, imagenes.Length)];
            imagen.rectTransform.sizeDelta = imagen.rectTransform.sizeDelta * 2;
        }
        
        
    }
}
