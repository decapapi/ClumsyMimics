using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Victoria : MonoBehaviour
{
    public InputField input;
    public int puntuacion;
    private ControlEscenas controlEscenas;

    public GameObject texto;

    public GameObject controlador;
    public ControlGlobal controlGlobalScript;

    void Start()
    {
        controlEscenas = FindObjectOfType<ControlEscenas>();
        controlGlobalScript = FindObjectOfType<ControlGlobal>();
        if (controlGlobalScript != null)
        {
            puntuacion = controlGlobalScript.Dinero;
            texto.GetComponent<Text>().text = puntuacion.ToString();
        }
        else
        {
            GameObject nuevoControl = Instantiate(controlador);
            controlGlobalScript = nuevoControl.GetComponent<ControlGlobal>();
        }
    }

    public void CambiarEscena()
    {
        if (string.IsNullOrEmpty(input.text))
            return;
        
        GuardarPuntuacion(input.text, puntuacion);

        controlGlobalScript.Resetear();

        controlEscenas.CargarEscena("Inicio");
    }

    private void GuardarPuntuacion(string nombreJugador, int puntuacion)
    {
        string rutaArchivo = "puntuaciones.txt";

        // Crear o abrir el archivo
        using (StreamWriter writer = new StreamWriter(rutaArchivo, true))
        {
            // Escribir la puntuación en formato "nombre:puntuacion"
            writer.WriteLine($"{nombreJugador}:{puntuacion}");
        }
    }
}
