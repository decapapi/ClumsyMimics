using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlPedidos : MonoBehaviour
{
    public Transform bocadilloPedidoRandom;
    public Transform bocadilloPedidoNormal;
    public Image imagenPedidoNormal;
    public Image imagenResultado;
    public Image imagenMaterial1;
    public Image imagenMaterial2;
    private string productoPedido;
    private GeneradorNPCs generadorNPCs;

    void Start()
    {
        generadorNPCs = GameObject.Find("Generador NPCs").GetComponent<GeneradorNPCs>();
        bocadilloPedidoRandom.localScale = Vector3.zero;
        bocadilloPedidoNormal.localScale = Vector3.zero;
    }

    public void GenerarPedidoRandom()
    {
        bocadilloPedidoRandom.localScale = Vector3.one;
    }

    public void GenerarPedidoNormal()
    {
        bocadilloPedidoNormal.localScale = Vector3.one;

        string[] numeros = { "12", "13", "14", "23", "24", "34" };
        productoPedido = numeros[Random.Range(0, numeros.Length)];
        Sprite pedido = Resources.Load<Sprite>("Arte/provisional/Objetosprovisionales/" + productoPedido);

        imagenPedidoNormal.sprite = pedido;
        imagenPedidoNormal.enabled = true;
    }

    public void VenderProductoRandom()
    {
        if (bocadilloPedidoRandom.localScale == Vector3.zero)
            return;

        bool productoVendido = false;

        if (imagenResultado.sprite != null)
        {
            imagenResultado.sprite = null;
            imagenResultado.enabled = false;
            productoVendido = true;
        }
        
        if (imagenMaterial1.sprite != null)
        {
            imagenMaterial1.sprite = null;
            imagenMaterial1.enabled = false;
            productoVendido = true;
        }

        if (imagenMaterial2.sprite != null)
        {
            imagenMaterial2.sprite = null;
            imagenMaterial2.enabled = false;
            productoVendido = true;
        }

        if (productoVendido)
            CerrarPedidoRandom();
    }

    public void VenderProductoNormal()
    {
        if (bocadilloPedidoNormal.localScale == Vector3.zero)
            return;

        if (imagenResultado.sprite == null)
        return;

        else if (imagenResultado.sprite.name == productoPedido)
        {
            imagenResultado.sprite = null;
            imagenResultado.enabled = false;
            CerrarPedidoNormal();
        }
    }

    public void CerrarPedidoRandom()
    {
        bocadilloPedidoRandom.localScale = Vector3.zero;
    }

    public void CerrarPedidoNormal()
    {
        bocadilloPedidoNormal.localScale = Vector3.zero;
        imagenPedidoNormal.enabled = false;
    }
}
