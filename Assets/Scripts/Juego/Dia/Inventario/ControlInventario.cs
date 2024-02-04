using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlInventario : MonoBehaviour
{
    public Image[] inventoryIcons;
    public Transform transformJugador;
    public GameObject item1;
    public GameObject item2;
    public GameObject item3;
    public GameObject item4;
    private int slotsMaximos = 8;
    private bool jugadorEnElInventario = false;

    public GameObject controlador;
    public ControlGlobal controlGlobalScript;

    private class ObjetoDeInventario
    {
        public int itemId;
        public string tipo;
        public int slot;
    }

    
    private List<ObjetoDeInventario> objetosAlmacenados = new List<ObjetoDeInventario>();

    void Start()
    {
        controlGlobalScript = FindObjectOfType<ControlGlobal>();
        if (controlGlobalScript != null)
        {
            for (int i = 0; i < controlGlobalScript.ObjetosGuardados.Length; i++)
            {
                if (controlGlobalScript.ObjetosGuardados[i] != null && controlGlobalScript.ObjetosGuardados[i] != "" && controlGlobalScript.ObjetosGuardados[i] != "0")
                {
                    AnyadirObjeto(controlGlobalScript.ObjetosGuardados[i], i + 9999);
                }
                
            }
        }
        else
        {
            GameObject nuevoControl = Instantiate(controlador);
            controlGlobalScript = nuevoControl.GetComponent<ControlGlobal>();
        }
    }

    public void AnyadirObjetoExterno(string tipo, int id)
    {
        AnyadirObjeto(tipo, id);
        controlGlobalScript.ObjetosGuardados[id] = tipo;
    }
    public void AnyadirObjeto(string tipo, int id)
    {
        if (InventarioLleno() || ItemExiste(id))
            return;

        for (int i = 0; i < slotsMaximos; i++)
        {
            if (inventoryIcons[i].sprite == null)
            {
                inventoryIcons[i].sprite = Resources.Load<Sprite>("Arte/Items/" + tipo);
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
        }

        Vector3 posDropear;
        do {
            posDropear = transformJugador.position + Random.insideUnitSphere * 2f;
        } while (Vector3.Distance(posDropear, transformJugador.position) < 1f);

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
