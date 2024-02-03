using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetosTienda : MonoBehaviour
{
    private bool zoomed = false;
    public bool jugada = false;
    public float actx;
    public float acty;
    public GameObject gestorObj;
    public Vector3 posaux;
    public int valor;
    public int puntuacion;

    public void SeleccionarSpriteObjeto(int obj)
    {
        Sprite spriteObjeto;
        // 2. Seleccionar el sprite del objeto
        spriteObjeto = Resources.Load<Sprite>("Arte/provisional/Objetosprovisionales/" + obj.ToString());
       
        // 3. Asignar el sprite al objeto
        GetComponent<SpriteRenderer>().sprite = spriteObjeto;
    }

    public void AsignarAtributos (int obj)
    {
        switch (obj)
        {
            case 1:
                valor = 1;
                puntuacion = 100;
                break;
            case 2:
                valor = 1;
                puntuacion = 100;
                break;
            case 3:
                valor = 1;
                puntuacion = 100;
                break;
            case 4:
                valor = 1;
                puntuacion = 100;
                break;
            case 12:
                valor = 2;
                puntuacion = 500;
                break;
            case 13:
                valor = 2;
                puntuacion = 500;
                break;
            case 14:
                valor = 2;
                puntuacion = 500;
                break;
            case 23:
                valor = 2;
                puntuacion = 500;
                break;
            case 24:
                valor = 2;
                puntuacion = 500;
                break;
            case 34:
                valor = 2;
                puntuacion = 500;
                break;
        }
        SeleccionarSpriteObjeto(obj);
    }

    public void CambiarDimSprite(float dimx, float dimy)
    {
        transform.localScale = new Vector3(dimx, dimy, 1);
    }

    public void CambiarPosSprite(float posx, float posy, float posz = 0)
    {
        transform.position = new Vector3(posx, posy, posz); 
    }

    public void CambiarAusada(Vector3 pos)
    {
        transform.position = pos;
    }
    public void zoom()
    {
        
        if (!jugada){
            if (zoomed == false)
            {
                CambiarPosSprite(0, 0);
                CambiarDimSprite(200f, 200f);
                
                zoomed = true;
            }
            else
            {
                CambiarPosSprite(actx, acty);
                CambiarDimSprite(100f, 100f);
                
                zoomed = false;
            }
        }else if (jugada)
        {
            if (zoomed == false)
            {
                actx = (float)transform.position.x;
                acty = (float)transform.position.y;
                CambiarPosSprite(0, 0, -1);
                CambiarDimSprite(100f, 100f);
                
                zoomed = true;
            }
            else
            {
                CambiarPosSprite(actx, acty);
                CambiarDimSprite(26f, 26f);
                
                zoomed = false;
            }
        }
              
    }

    public void jugarobj(Vector3 pos)
    {
        
        if (!jugada)
        {
            CambiarAusada(pos);    
            CambiarDimSprite(26f, 26f);
            jugada = true;
        }
        
    }


    // Start is called before the first frame update
    void Start()
    {
        actx = (float)transform.position.x;
        acty = (float)transform.position.y; 
        GestorDeObjetosEnTienda gestorscript = gestorObj.GetComponent<GestorDeObjetosEnTienda>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
