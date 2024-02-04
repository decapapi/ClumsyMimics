using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlDia : MonoBehaviour
{
    public float tiempoRestante = 180f;
    public Text contador;
    private DateTime horaActual;
    private bool isGamePaused = false;
    public GameObject menuPausa;
    private static int dia = 0;
    public static int BalasTotales {get;set;} = 5;
    private ControlHUD controlHUD;

    private const float tiempoTotalJuego = 180f;
    private const string horaInicio = "08:00";
    private const string horaFin = "20:00";

    public GameObject controlador;
    public ControlGlobal controlGlobalScript;

    void Start()
    {
        horaActual = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 0, 0);
        controlHUD = GameObject.Find("HUD").GetComponent<ControlHUD>();
        InvokeRepeating("ActualizarContador", 0f, 1f);
        dia++;
        InvokeRepeating("ActualizarHora", 0f, 0.25f);

        
    }
    void Awake()
    {
        controlGlobalScript = FindObjectOfType<ControlGlobal>();
        
        if (controlGlobalScript != null)
        {
            BalasTotales = controlGlobalScript.Balas;
        }else{
            GameObject nuevoControl = Instantiate(controlador);
            controlGlobalScript = nuevoControl.GetComponent<ControlGlobal>();
        }
    }
    public int RecuentoBalas()
    {
        return BalasTotales;
    }
    public void RestarBalas()
    {
        controlHUD.QuitarBala();
        BalasTotales--;
        controlGlobalScript.Balas = BalasTotales;
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
            tiempoRestante--;

            if (tiempoRestante <= 0f)
                SceneManager.LoadScene("Gameover");
        }
    }

    void ActualizarHora()
    {
        contador.text = horaActual.ToString("HH:mm") + "h";
        horaActual = horaActual.AddMinutes(1);
    }
}
