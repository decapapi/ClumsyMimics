using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlDia : MonoBehaviour
{
    public float tiempoRestante;
    public Text contador;
    private bool isGamePaused = false;
    public GameObject menuPausa;

    void Start()
    {
        InvokeRepeating("ActualizarContador", 0f, 1f);
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
        tiempoRestante -= 1f;

        contador.text = "Tiempo restante: " + tiempoRestante.ToString() + "s";

        if (tiempoRestante <= 0)
            SceneManager.LoadScene("Gameover");
    }
}
