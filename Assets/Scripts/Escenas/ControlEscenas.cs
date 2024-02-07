using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ControlEscenas : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    public GameObject controlador;
    public ControlGlobal controlGlobalScript;
    public int dias = 0;
    private void Start() {
        AudioSource audio = GetComponent<AudioSource>();
        if (audio != null)
            audio.Play();
        
    }
    void Awake()
    {
        controlGlobalScript = FindObjectOfType<ControlGlobal>();
        if (controlGlobalScript != null)
        {
            dias = controlGlobalScript.dias;
        }else{
            GameObject nuevoControl = Instantiate(controlador);
            controlGlobalScript = nuevoControl.GetComponent<ControlGlobal>();
        }
    }

    public void CargarEscena(string nombreEscena)
    {
        if (nombreEscena.Equals("Inicio") || nombreEscena.Equals("Victoria") || nombreEscena.Equals("Gameover")) 
        {
            controlGlobalScript.Resetear();
        }

        if (dias == 3)
        {
            StartCoroutine(Transition("Victoria"));
            controlGlobalScript.dias++;
        }
        else{
            StartCoroutine(Transition(nombreEscena));
        }
        
    }

    IEnumerator Transition (string nombreEscena)
    {
        transition.SetTrigger("Inicio");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(nombreEscena);
    }
}
