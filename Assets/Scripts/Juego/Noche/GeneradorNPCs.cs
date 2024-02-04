using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorNPCs : MonoBehaviour
{
    public float tiempoSpawn = 10f;
    public int maxNPCs = 10;
    public int npcsSpawneados = 2;
    public GameObject npc;
    public Transform random;
    public Transform normal;
    private Vector3 randomDestPos = new Vector3(350, 300, 1);
    private Vector3 normalDestPos = new Vector3(600, 300, 1);
    private ControlPedidos controlPedidos;
    private GameObject randomNPC;
    private GameObject normalNPC;

    void Start()
    {
        controlPedidos = GameObject.Find("Pedidos").GetComponent<ControlPedidos>();
        StartCoroutine(SpawnearNPCConDelay(true, Random.Range(4, 7)));
        StartCoroutine(SpawnearNPCConDelay(false, Random.Range(4, 7)));
    }

    void SpawnearNPC(bool esRandom = false)
    {
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
    }

    public void BorrarNPC(bool randomNPC)
    {
        StartCoroutine(AnimacionBorrarNPC(randomNPC));
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

    public IEnumerator SpawnearNPCConDelay(bool randomNPC, float delay)
    {
        yield return new WaitForSeconds(delay);
        SpawnearNPC(randomNPC);
    }
}
