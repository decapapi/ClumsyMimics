using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PantallaClasificacion : MonoBehaviour
{
    public Text[] textoPuntuaciones;

    public class PlayerInfo
    {
        public string Nombre { get; set; }
        public int Puntuacion { get; set; }
        
        public PlayerInfo(string nombre, int puntuacion)
        {
            Nombre = nombre;
            Puntuacion = puntuacion;
        }
    }

    void Start()
    {
        MostrarPuntuaciones();
    }

    public void MostrarPuntuaciones()
    {
        string rutaArchivo = "puntuaciones.txt";

        // Verificar si el archivo existe
        if (File.Exists(rutaArchivo))
        {
            // Leer todas las líneas del archivo
            string[] lineas = File.ReadAllLines(rutaArchivo);

            // Convertir las líneas a lista de PlayerInfo
            List<PlayerInfo> puntuaciones = lineas.Select(linea =>
            {
                string[] partes = linea.Split(':');
                return new PlayerInfo(partes[0], int.Parse(partes[1]));
            }).ToList();

            // Ordenar las puntuaciones por valor de forma descendente
            var puntuacionesOrdenadas = puntuaciones.OrderByDescending(playerInfo => playerInfo.Puntuacion);

            // Mostrar las 5 primeras puntuaciones
            int i = 0;
            foreach (var playerInfo in puntuacionesOrdenadas.Take(5))
            {
                textoPuntuaciones[i].text = $"{playerInfo.Nombre}: {playerInfo.Puntuacion}";
                i++;
            }
        }
    }
}
