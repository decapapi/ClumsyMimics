using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlRejillaMateriales : MonoBehaviour
{
    public Image material1;
    public Image material2;
    public Image resultado;
    public Image[] rejillas;
    private List<KeyValuePair<string, int>> armasAlmacenadas = new List<KeyValuePair<string, int>>();
    void Start()
    {
        AnyadirArmaARejilla("1");
        AnyadirArmaARejilla("2");
        AnyadirArmaARejilla("3");
        AnyadirArmaARejilla("4");
        AnyadirArmaARejilla("4");
        AnyadirArmaARejilla("3");
        AnyadirArmaARejilla("2");
        AnyadirArmaARejilla("1");
    }

    public void AnyadirArmaARejilla(string arma)
    {
        if (InventarioLleno())
            return;

        for (int i = 0; i < 8; i++) 
        {
            if (rejillas[i].sprite == null)
            {
                rejillas[i].sprite = Resources.Load<Sprite>("Arte/Items/" + arma);
                rejillas[i].enabled = true;
                armasAlmacenadas.Add(new KeyValuePair<string, int>(arma, i));
                break;
            }
        }
    }

    public void AnadirArmaAMesa(int slot)
    {
        if (resultado.sprite != null)
            return;

        if (slot < 0 || slot >= rejillas.Length)
            return;

        if (material1.sprite != null && material2.sprite != null)
            return;

        if (rejillas[slot].sprite == null)
            return;

        string arma = rejillas[slot].sprite.name;
        KeyValuePair<string, int>? armaAlmacenada = null;

        foreach (var storedWeapon in armasAlmacenadas)
        {
            if (storedWeapon.Key == arma)
            {
                armaAlmacenada = storedWeapon;
                break;
            }
        }

        if (armaAlmacenada == null)
            return;

        armasAlmacenadas.Remove(armaAlmacenada.Value);
        rejillas[slot].sprite = null;
        rejillas[slot].enabled = false;

        if (material1.sprite == null)
        {
            material1.sprite = Resources.Load<Sprite>("Arte/Items/" + arma);
            material1.enabled = true;
        }
        else if (material2.sprite == null)
        {
            material2.sprite = Resources.Load<Sprite>("Arte/Items/" + arma);
            material2.enabled = true;
        }
    }

    public void CancelarCrafteo()
    {
        if (resultado.sprite != null)
            return;

        if (material1.sprite != null)
        {
            AnyadirArmaARejilla(material1.sprite.name);
            material1.sprite = null;
            material1.enabled = false;
        }
        if (material2.sprite != null)
        {
            AnyadirArmaARejilla(material2.sprite.name);
            material2.sprite = null;
            material2.enabled = false;
        }

        resultado.sprite = null;
    }

    public void Craftear()
    {
        if (material1.sprite == null || material2.sprite == null)
            return;

        string arma1 = material1.sprite.name;
        string arma2 = material2.sprite.name;

        Sprite imagenResultado = Resources.Load<Sprite>("Arte/Items/" + arma1 + arma2);

        if (imagenResultado == null)
            imagenResultado = Resources.Load<Sprite>("Arte/Items/" + arma2 + arma1);
            
        resultado.sprite = imagenResultado;
        resultado.enabled = true;

        material1.sprite = null;
        material1.enabled = false;
        material2.sprite = null;
        material2.enabled = false;
    }

    public bool InventarioLleno()
    {
        return armasAlmacenadas.Count >= 8;
    }
}
