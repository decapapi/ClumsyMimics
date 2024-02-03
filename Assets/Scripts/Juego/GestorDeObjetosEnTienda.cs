using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class GestorDeObjetosEnTienda : MonoBehaviour
{
    public GestionTablero gestionTableroScript;
    public ObjetosTienda objetosTiendaScript;

    public GameObject objetoPrefab;

    public GameObject tableroObjeto;

    public int puntuacionTotal = 0;

    public List<string> objetosGuardados = new List<string>();
    public List<GameObject> objetosEnLista = new List<GameObject>();
    public List<GameObject> objetosJugados = new List<GameObject>();
    public Transform canvas;


    public Vector3 nuevaPosicion(ObjetosTienda obj)
    {
        if (!obj.jugada){
            if (objetosJugados.Count < 3 && obj.valor == 1)
            {
                obj.jugada = true;
                objetosJugados.Add(obj.gameObject);
                objetosEnLista.Remove(obj.gameObject);
                if (objetosJugados.Count == 1)
                {
                    return gestionTableroScript.waypoints[9].position;
                }
                else if (objetosJugados.Count == 2)
                {
                    return gestionTableroScript.waypoints[10].position;
                }else{
                    return obj.transform.position;
                }
                
            }else if (objetosJugados.Count == 0 && obj.valor == 2)
            {
                obj.jugada = true;
                objetosJugados.Add(obj.gameObject);
                objetosEnLista.Remove(obj.gameObject);
                return gestionTableroScript.waypoints[11].position;
            }else
            {
                return obj.transform.position;
            }
        }else
        {
            obj.jugada = false;
            objetosJugados.Remove(obj.gameObject);
            objetosEnLista.Add(obj.gameObject);
            return gestionTableroScript.waypoints[objetosEnLista.Count-1].position;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        int posición = 0;
        foreach (string objeto in objetosGuardados)
        {
            GameObject obj = Instantiate(objetoPrefab, tableroObjeto.GetComponent<GestionTablero>().waypoints[posición].position, Quaternion.identity);
            obj.transform.SetParent(canvas);
            obj.GetComponent<ObjetosTienda>().AsignarAtributos(int.Parse(objeto));
            obj.GetComponent<ObjetosTienda>().gestorObj = this.gameObject;
            objetosEnLista.Add(obj);
            Debug.Log("objeto: " + objeto);
            posición++;
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
