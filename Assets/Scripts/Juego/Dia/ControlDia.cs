using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlDia : MonoBehaviour
{
    public float tiempoRestante = 0f;
    public Text contador;
    private bool isGamePaused = false;
    public GameObject menuPausa;
    private static int dia = 0;
    public static int BalasTotales { get; set; } = 5;

    void Start()
    {
        InvokeRepeating("ActualizarContador", 0f, 1f);
        dia++;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseGame();
        }
    }

    public void TogglePauseGame()
    {
        isGamePaused = !isGamePaused;

        if (isGamePaused)
        {
            Time.timeScale = 0f;
            menuPausa.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            menuPausa.SetActive(false);
        }
    }

    void ActualizarContador()
    {
        tiempoRestante += 1f;
        tiempoRestante = Mathf.Min(18000f, tiempoRestante);

        // Calcular la hora actual en el formato de 24 horas
        int horasActuales = Mathf.FloorToInt(tiempoRestante / 3600);
        int minutosActuales = Mathf.FloorToInt((tiempoRestante % 3600) / 60);

        // Calcular la hora de inicio del juego
        int horaInicio = 8;

        // Calcular la hora final del juego
        int horaFinal = 20;

        // Calcular la hora actual progresiva
        int horaProgresiva = horaInicio + horasActuales;

        // Formatear el tiempo como texto
        string tiempoFormateado = string.Format("{0:D2}:{1:D2}h", horaProgresiva, minutosActuales);

        // Actualizar el texto en pantalla
        contador.text = tiempoFormateado;

        if (tiempoRestante >= 18000)
        {
            SceneManager.LoadScene("Gameover");
        }
    }
}
