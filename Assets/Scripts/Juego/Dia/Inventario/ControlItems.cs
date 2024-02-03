using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlItems : MonoBehaviour
{
    public string tipoDeItem;
    public float cooldownRecoger = 0.5f;
    private ControlInventario inventario;
    private bool collectable = false;
    private static int id = 0;
    void Start()
    {
        id++;
        inventario = FindObjectOfType<ControlInventario>();
        StartCoroutine(StartConsumeCooldown());
    }

    private void OnTriggerStay2D(Collider2D other) 
    {
        if (!other.CompareTag("Player"))
            return;

        if (!collectable || inventario.InventarioLleno())
            return;

        inventario.AnyadirObjeto(tipoDeItem, id);
        Destroy(gameObject);
    }

    private IEnumerator StartConsumeCooldown()
    {
        if (collectable)
        yield return false;

        yield return new WaitForSeconds(cooldownRecoger);
        collectable = true;
    }
}
