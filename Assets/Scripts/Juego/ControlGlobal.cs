using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ControlGlobal : MonoBehaviour
{
    public string[] ObjetosGuardados { get; set; } = new string[8];
    public int NumeroObjetos { get; set; } = 0;
    public  int Balas { get; set; } = 5;
    public  int Vidas { get; set; } = 5;
    public  int Dinero { get; set; } = 0;

    public int dias { get; set; } = 0;

    private void Awake() 
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public bool AnyadirObjeto(string objeto)
    {
        if (NumeroObjetos >= 8)
            return false;

        ObjetosGuardados[NumeroObjetos] = objeto;
        NumeroObjetos++;
        return true;
    }

    public void QuitarObjeto(string objeto)
    {
        for (int i = 0; i < ObjetosGuardados.Length; i++)
        {
            if (ObjetosGuardados[i] == objeto)
            {
                ObjetosGuardados[i] = "";
                NumeroObjetos--;
                break;
            }
        }

        Array.Sort(ObjetosGuardados);
        Array.Reverse(ObjetosGuardados);
    }

    public void VaciarObjetos()
    {
        ObjetosGuardados = new string[8];
        NumeroObjetos = 0;
    }

    public void Resetear()
    {
        VaciarObjetos();
        Balas = 5;
        Vidas = 5;
        Dinero = 0;
        dias = 0;
    }
}
