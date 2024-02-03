using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlEscenas : MonoBehaviour
{
    private void Start() {
        AudioSource audio = GetComponent<AudioSource>();
        if (audio != null)
            audio.Play();
    }

    public void CargarEscena(string nombreEscena)
    {
        SceneManager.LoadScene(nombreEscena);
    }
}
