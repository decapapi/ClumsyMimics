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
    private bool juegoPausado = false;
    public GameObject menuPausa;
    public GameObject inventario;
    public GameObject minimapa;
    private static int dia = 0;
    public static int BalasTotales {get;set;} = 5;
    private ControlHUD controlHUD;
    private AudioSource musicaDia;

    public GameObject controlador;
    public ControlGlobal controlGlobalScript;

    void Start()
    {
        horaActual = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 0, 0);
        controlHUD = GameObject.Find("HUD").GetComponent<ControlHUD>();
        musicaDia = GetComponent<AudioSource>();
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
            controlGlobalScript.dias++;
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
        juegoPausado = !juegoPausado;

        if (juegoPausado)
        {
            Time.timeScale = 0f;
            menuPausa.SetActive(true);
            inventario.SetActive(false);
            minimapa.SetActive(false);
            musicaDia.Pause();
        }
        else
        {
            Time.timeScale = 1f;
            menuPausa.SetActive(false);
            inventario.SetActive(true);
            minimapa.SetActive(true);
            musicaDia.UnPause();
        }
    }

    void ActualizarContador()
    {
        if (!juegoPausado)
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

    public bool JuegoPausado()
    {
        return juegoPausado;
    }
}
