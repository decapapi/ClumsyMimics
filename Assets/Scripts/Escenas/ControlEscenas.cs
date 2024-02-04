using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlEscenas : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;
    private void Start() {
        AudioSource audio = GetComponent<AudioSource>();
        if (audio != null)
            audio.Play();
    }

    public void CargarEscena(string nombreEscena)
    {
        StartCoroutine(Transition(nombreEscena));
    }

    IEnumerator Transition (string nombreEscena)
    {
        transition.SetTrigger("Inicio");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(nombreEscena);
    }
}
