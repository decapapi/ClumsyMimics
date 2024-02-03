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
    private Vector3 randomDestPos = new Vector3(350, 250, 1);
    private Vector3 normalDestPos = new Vector3(600, 250, 1);
    private ControlPedidos controlPedidos;

    void Start()
    {
        controlPedidos = GameObject.Find("Pedidos").GetComponent<ControlPedidos>();
        StartCoroutine(SpawnearNPCConDelay(true, Random.Range(5, 10)));
        StartCoroutine(SpawnearNPCConDelay(false, Random.Range(5, 10)));
    }

    void SpawnearNPC(bool randomNPC = false)
    {
        GameObject nuevoNPC = Instantiate(npc, randomNPC ? random.position : normal.position, Quaternion.identity);
        nuevoNPC.transform.SetParent(transform);
        StartCoroutine(MoverNPC(nuevoNPC.transform, randomNPC ? randomDestPos : normalDestPos, randomNPC));
    }

    IEnumerator MoverNPC(Transform npcTransform, Vector3 destino, bool randomNPC)
    {
        float elapsedTime = 0f;
        float tiempoMovimiento = 5f;
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

    IEnumerator SpawnearNPCConDelay(bool randomNPC, float delay)
    {
        yield return new WaitForSeconds(delay);
        SpawnearNPC(randomNPC);
    }
}
