using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneradorNPCs : MonoBehaviour
{
    public float tiempoSpawn = 10f;
    public int maxNPCs;
    public GameObject npc;
    public Transform random;
    public Transform normal;
    public Text textoClientes;
    private int npcsSpawneados = 0;
    private Vector3 randomDestPos = new Vector3(350, 300, 1);
    private Vector3 normalDestPos = new Vector3(600, 300, 1);
    private ControlPedidos controlPedidos;
    private GameObject randomNPC;
    private GameObject normalNPC;

    void Start()
    {
        maxNPCs = Random.Range(4, 12);
        controlPedidos = GameObject.Find("Pedidos").GetComponent<ControlPedidos>();
        StartCoroutine(SpawnearNPCConDelay(true, Random.Range(1, 5)));
        StartCoroutine(SpawnearNPCConDelay(false, Random.Range(1, 5)));
        ActualizarTextoClientes();
    }

    void SpawnearNPC(bool esRandom = false)
    {
        if (npcsSpawneados >= maxNPCs)
            return;

        if (esRandom)
        {
            randomNPC = Instantiate(npc, random.position, Quaternion.identity);
            randomNPC.transform.SetParent(transform);
            StartCoroutine(MoverNPC(randomNPC.transform, randomDestPos, true));
        }
        else
        {
            normalNPC = Instantiate(npc, normal.position, Quaternion.identity);
            normalNPC.transform.SetParent(transform);
            StartCoroutine(MoverNPC(normalNPC.transform, normalDestPos, false));
        }

        npcsSpawneados++;
        ActualizarTextoClientes();
    }

    public void BorrarNPC(bool randomNPC)
    {
        StartCoroutine(AnimacionBorrarNPC(randomNPC));
    }

    private void ActualizarTextoClientes()
    {
        textoClientes.text = "Clientes potenciales: " + (maxNPCs - npcsSpawneados);
    }

    IEnumerator AnimacionBorrarNPC(bool esRandom)
    {
        GameObject objetoNpc = esRandom ? randomNPC : normalNPC;

        float elapsedTime = 0f;
        float tiempoMovimiento = 3f;
        Vector3 startingPos = objetoNpc.transform.position;
        Vector3 destino = esRandom ? random.position : normal.position;

        while (elapsedTime < tiempoMovimiento)
        {
            if (objetoNpc == null)
                yield break;
            objetoNpc.transform.position = Vector3.Lerp(startingPos, destino, elapsedTime / tiempoMovimiento);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        objetoNpc.transform.position = destino;

        Destroy(objetoNpc);
    }

    IEnumerator MoverNPC(Transform npcTransform, Vector3 destino, bool randomNPC)
    {
        float elapsedTime = 0f;
        float tiempoMovimiento = 4f;
        Vector3 startingPos = npcTransform.position;

        while (elapsedTime < tiempoMovimiento)
        {
            npcTransform.position = Vector3.Lerp(startingPos, destino, elapsedTime / tiempoMovimiento);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        npcTransform.position = destino;
        if (randomNPC)
            controlPedidos.GenerarPedidoRandom();
        else
            controlPedidos.GenerarPedidoNormal();
    }

    public bool randomNPCActivo()
    {
        return randomNPC != null;
    }

    public bool normalNPCActivo()
    {
        return normalNPC != null;
    }

    public IEnumerator SpawnearNPCConDelay(bool randomNPC, float delay)
    {
        yield return new WaitForSeconds(delay);
        SpawnearNPC(randomNPC);
    }
}
