using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Victoria : MonoBehaviour
{
    public InputField input;
    public int puntuacion;

    public GameObject texto;

    public GameObject controlador;
    public ControlGlobal controlGlobalScript;
    private ControlEscenas controlEscenas;
    private bool puntacionGuardada = false;
    void Start()
    {
        controlEscenas = FindObjectOfType<ControlEscenas>();
        controlGlobalScript = FindObjectOfType<ControlGlobal>();
        if (controlGlobalScript != null)
        {
            puntuacion = controlGlobalScript.Dinero;
            texto.GetComponent<Text>().text = puntuacion.ToString("D5");
        }
        else
        {
            GameObject nuevoControl = Instantiate(controlador);
            controlGlobalScript = nuevoControl.GetComponent<ControlGlobal>();
        }
    }

    public void GuardarPuntuacion()
    {   
        if (puntacionGuardada)
            return;
        puntacionGuardada = true;
        GuardarPuntuacion(string.IsNullOrEmpty(input.text) ? "Desconocido" : input.text, puntuacion);
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
