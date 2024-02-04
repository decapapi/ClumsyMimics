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
    public static int dinero = 0;
    public Text dineroTexto;
    private AudioSource sonidoVender;

    public GameObject controlador;
    public ControlGlobal controlGlobalScript;

    void Start()
    {
        sonidoVender = GameObject.Find("Vender").GetComponent<AudioSource>();
        generadorNPCs = GameObject.Find("Generador NPCs").GetComponent<GeneradorNPCs>();
        bocadilloPedidoRandom.localScale = Vector3.zero;
        bocadilloPedidoNormal.localScale = Vector3.zero;

        controlGlobalScript = FindObjectOfType<ControlGlobal>();
        if (controlGlobalScript != null)
        {
            dinero = controlGlobalScript.Dinero;          
        }else{
            GameObject nuevoControl = Instantiate(controlador);
            controlGlobalScript = nuevoControl.GetComponent<ControlGlobal>();
        }
    }
    void Update()
    {
        ActualizarDinerotexto();
    }

    void ActualizarDinerotexto()
    {
        dineroTexto.text = "Ahorros: " + dinero.ToString() + " pesetas";
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
        Sprite pedido = Resources.Load<Sprite>("Arte/Items/" + productoPedido);

        imagenPedidoNormal.sprite = pedido;
        imagenPedidoNormal.enabled = true;
    }

    public void VenderProductoRandom()
    {
        if (bocadilloPedidoRandom.localScale == Vector3.zero)
            return;

        bool productoVendido = false;

        if (imagenResultado.sprite != null) // 75
        {
            imagenResultado.sprite = null;
            imagenResultado.enabled = false;
            productoVendido = true;
            dinero += 75;
        }
        if (imagenMaterial1.sprite != null) // 25
        {
            imagenMaterial1.sprite = null;
            imagenMaterial1.enabled = false;
            productoVendido = true;
            dinero += 25;
        }

        if (imagenMaterial2.sprite != null) // 25
        {
            imagenMaterial2.sprite = null;
            imagenMaterial2.enabled = false;
            productoVendido = true;
            dinero += 25;
        }

        if (productoVendido) {
            sonidoVender.Play();
            CerrarPedidoRandom();
            generadorNPCs.BorrarNPC(true);
            StartCoroutine(generadorNPCs.SpawnearNPCConDelay(true, Random.Range(4, 7)));
        }

        controlGlobalScript.Dinero = dinero;
    }

    public void VenderProductoNormal()
    {
        if (bocadilloPedidoNormal.localScale == Vector3.zero)
            return;

        if (imagenResultado.sprite == null)
        return;

        if (imagenResultado.sprite.name == productoPedido) // 250
        {
            dinero += 250;
            sonidoVender.Play();
            imagenResultado.sprite = null;
            imagenResultado.enabled = false;
            CerrarPedidoNormal();
            generadorNPCs.BorrarNPC(false);
            StartCoroutine(generadorNPCs.SpawnearNPCConDelay(false, Random.Range(4, 7)));
        }

        controlGlobalScript.Dinero = dinero;
    }

    public void CancelarPedidoRandom()
    {
        if (generadorNPCs.randomNPCActivo() && bocadilloPedidoRandom.localScale != Vector3.zero) {
            CerrarPedidoRandom();
            generadorNPCs.BorrarNPC(true);
            StartCoroutine(generadorNPCs.SpawnearNPCConDelay(true, Random.Range(4, 7)));
        }
    }

    public void CancelarPedidoNormal()
    {
        if (generadorNPCs.normalNPCActivo() && bocadilloPedidoNormal.localScale != Vector3.zero) {
            CerrarPedidoNormal();
            generadorNPCs.BorrarNPC(false);
            StartCoroutine(generadorNPCs.SpawnearNPCConDelay(false, Random.Range(4, 7)));
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
