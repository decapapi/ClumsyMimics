using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlGlobal : MonoBehaviour
{
    public static string[] ObjetosGuardados { get; set; } = new string[8];
    public static int Balas { get; set; } = 5;
    public static int Vidas { get; set; } = 5;
    public static int Dinero { get; set; } = 0;

    private void Awake() 
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public static void Resetear()
    {
        ObjetosGuardados = new string[8];
        Balas = 5;
        Vidas = 5;
        Dinero = 0;
    }
}
