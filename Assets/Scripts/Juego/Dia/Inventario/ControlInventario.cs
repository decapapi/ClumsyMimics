using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlInventario : MonoBehaviour
{
    public int slotsMaximos = 8;
    public Image[] inventoryIcons;
    public Transform transformJugador;
    public GameObject item1;
    public GameObject item2;
    public GameObject item3;
    public GameObject item4;
    public GameObject item5;
    public GameObject item6;
    public GameObject item7;
    public GameObject item8;
    public GameObject item9;
    public GameObject item10;
    private bool jugadorEnElInventario = false;

    private class ObjetoDeInventario
    {
        public int itemId;
        public string tipo;
        public int slot;
    }

    private List<ObjetoDeInventario> objetosAlmacenados = new List<ObjetoDeInventario>();

    void Start()
    {
        
    }

    public void AnyadirObjeto(string tipo, int id)
    {
        if (InventarioLleno() || ItemExiste(id))
            return;

        for (int i = 0; i < slotsMaximos; i++)
        {
            if (inventoryIcons[i].sprite == null)
            {
                inventoryIcons[i].sprite = Resources.Load<Sprite>("Arte/provisional/Objetosprovisionales/" + tipo);
                inventoryIcons[i].enabled = true;
                objetosAlmacenados.Add(new ObjetoDeInventario { itemId = id, tipo = tipo, slot = i });
                break;
            }
        }
    }

    public void EliminarObjeto(int slot)
    {
        if (slot < 0 || slot > slotsMaximos)
            return;
        
        ObjetoDeInventario objetoEliminado = objetosAlmacenados.Find(obj => obj.slot == slot);

        if (objetoEliminado == null)
            return;

        inventoryIcons[slot].sprite = null;
        inventoryIcons[slot].enabled = false;
        objetosAlmacenados.Remove(objetoEliminado);
   
        GameObject itemPrefab = null;
        switch (objetoEliminado.tipo) {
            case "1":
                itemPrefab = item1;
                break;
            case "2":
                itemPrefab = item2;
                break;
            case "3":
                itemPrefab = item3;
                break;
            case "4":
                itemPrefab = item4;
                break;
            case "12":
                itemPrefab = item5;
                break;
            case "13":
                itemPrefab = item6;
                break;
            case "14":
                itemPrefab = item7;
                break;
            case "23":
                itemPrefab = item8;
                break;
            case "24":
                itemPrefab = item9;
                break;
            case "34":
                itemPrefab = item10;
                break;
        }

        Vector3 posDropear;
        do {
            posDropear = transformJugador.position + Random.insideUnitSphere * 8f;
        } while (Vector3.Distance(posDropear, transformJugador.position) < 5f);

        Instantiate(itemPrefab, new Vector3(posDropear.x, posDropear.y, 0f), Quaternion.identity);
    }

    public bool ItemExiste(int itemId)
    {
        return objetosAlmacenados.Exists(obj => obj.itemId == itemId);
    }

    public bool InventarioLleno()
    {
        return objetosAlmacenados.Count >= slotsMaximos;
    }

    public bool JugadorEnElInventario()
    {
        return jugadorEnElInventario;
    }

     public void ToggleJugadorEnElInventario()
    {
        jugadorEnElInventario = !jugadorEnElInventario;
    }
}
