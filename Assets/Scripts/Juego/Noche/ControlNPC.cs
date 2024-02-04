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
        imagen = GetComponent<Image>();
        imagen.sprite = imagenes[Random.Range(0, imagenes.Length)];
    }
}
