using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EfectoLuz : MonoBehaviour
{
    public float duracionTransicion = 0.5f;
    private Image overlay;

    void Start()
    {
        overlay = GetComponent<Image>();
        StartCoroutine(Cambiar());
    }

    IEnumerator Cambiar()
    {
        while (true)
        {
            float alphaInicial = overlay.color.a;
            float alphaFinal = Random.Range(30f / 255f, 60f / 255f);

            float elapsedTime = 0f;

            while (elapsedTime < duracionTransicion)
            {
                overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, Mathf.Lerp(alphaInicial, alphaFinal, elapsedTime / duracionTransicion));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, alphaFinal);

            yield return new WaitForSeconds(duracionTransicion);
        }
    }
}
