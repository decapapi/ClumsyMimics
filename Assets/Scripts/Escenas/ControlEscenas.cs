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
        
        if (dias != 3)
        {
            StartCoroutine(Transition(nombreEscena));
        }else{
            controlGlobalScript.dias = 0;
            StartCoroutine(Transition("Victoria"));
        }
        
    }

    IEnumerator Transition (string nombreEscena)
    {
        transition.SetTrigger("Inicio");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(nombreEscena);
    }
}
