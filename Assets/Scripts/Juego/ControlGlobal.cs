using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlGlobal : MonoBehaviour
{
    public  string[] ObjetosGuardados { get; set; } = new string[8];
    public  int Balas { get; set; } = 5;
    public  int Vidas { get; set; } = 5;
    public  int Dinero { get; set; } = 0;

    public int dias { get; set; } = 0;

    private void Awake() 
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void anyadirobjeto()
    {
        
    }

    public void Resetear()
    {
        ObjetosGuardados = new string[8];
        Balas = 5;
        Vidas = 5;
        Dinero = 0;
        dias = 0;
    }
}
