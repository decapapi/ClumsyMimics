using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlDia : MonoBehaviour
{
    public float tiempoRestante = 180f; // Establece el tiempo total del juego en segundos (180 segundos = 3 minutos)
    public Text contador;
    private bool isGamePaused = false;
    public GameObject menuPausa;
    private static int dia = 0;
    public static int BalasTotales {get;set;} = 5;
    private ControlHUD controlHUD;

    private const float tiempoTotalJuego = 180f;
    private const string horaInicio = "08:00";
    private const string horaFin = "20:00";

    void Start()
    {
        controlHUD = GameObject.Find("HUD").GetComponent<ControlHUD>();
        InvokeRepeating("ActualizarContador", 0f, 1f);
        dia++;
    }

    public int RecuentoBalas()
    {
        return BalasTotales;
    }
    public void RestarBalas()
    {
        controlHUD.QuitarBala();
        BalasTotales--;
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
        if (!isGamePaused)
        {
            tiempoRestante -= Time.deltaTime;

            // Calcular horas y minutos a partir del tiempo restante
            int horas = Mathf.FloorToInt(tiempoRestante / 3600);
            int minutos = Mathf.FloorToInt((tiempoRestante % 3600) / 60);
            string tiempoFormateado = $"{horas:D2}:{minutos:D2}";

            // Actualizar el texto en pantalla
            contador.text = tiempoFormateado;

            if (tiempoRestante <= 0f)
            {
                // Si se acaba el tiempo, se acaba el juego
                SceneManager.LoadScene("Gameover");
            }
        }
    }
}
