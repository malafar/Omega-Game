using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;
using System.IO;

public class PuertasManager : SerializedMonoBehaviour {
    /// <summary>
    /// Lista de puertas del tablero.
    /// </summary>
    private List<Tuple<Coordenada, Orientacion>> puertas;

    /// <summary>
    /// Fichero que contiene los datos de coordenadas y orientación de todas las puertas.
    /// </summary>
    private string doorsData = "Assets/Heroes_Mystery/Ficheros/doorsData.txt";

    /// <summary>
    /// Método que carga los datos de las puertas desde el fichero de datos a la estructura correspondiente.
    /// </summary>
    public void loadPuertas() {
        puertas = new List<Tuple<Coordenada, Orientacion>>();

        // Lectura de datos
        try {
            StreamReader fichero = File.OpenText(doorsData);
            string puerta;

            using (fichero) {
                do {
                    puerta = fichero.ReadLine();

                    if (!String.IsNullOrEmpty(puerta)) {
                        string[] stats = puerta.Split(';');

                        addPuerta(stats);
                    }
                } while (puerta != null);
            }

            fichero.Close();
        }catch(Exception ex) {
            Debug.LogError(ex.StackTrace);
        }
    }

    /// <summary>
    /// Método encargado de añadir una puerta con los datos pasados.
    /// </summary>
    /// <param name="datos">Conjunto de cadenas de caracteres con los datos sobre las coordenadas de la casilla y de la orientación de la puerta.</param>
    private void addPuerta(string[] datos) {
        Coordenada coordenada = new Coordenada(int.Parse(datos[0]), int.Parse(datos[1]));
        Orientacion orientacion = parseOrientacion(datos[2]);

        Tuple<Coordenada, Orientacion> puerta = new Tuple<Coordenada, Orientacion>(coordenada, orientacion);

        puertas.Add(puerta);
    }

    /// <summary>
    /// Método de parseo de la orientación de un String a un enumeado de Orientación.
    /// </summary>
    /// <param name="sentido">Cadena de texto con la orientación a parsear.</param>
    /// <returns>Orientación parseada.</returns>
    private Orientacion parseOrientacion(string sentido) {
        
        switch (sentido) {
            case "U":
                return Orientacion.UP;

            case "R":
                return Orientacion.RIGHT;

            case "D":
                return Orientacion.DOWN;

            case "L":
                return Orientacion.LEFT;

            default:
                return Orientacion.NONE;
        }
    }

    /// <summary>
    /// Getter de la coordenada de una puerta de la lista de puertas.
    /// </summary>
    /// <param name="index">Índice de la puerta de la que se obtiene la coordenada.</param>
    /// <returns>Coordenada de la puerta.</returns>
    public Coordenada getCoordenadaPuerta(int index) {
        return puertas[index].Item1;
    }

    /// <summary>
    /// Getter de la orientación de una puerta de la lista de puertas.
    /// </summary>
    /// <param name="index">Índice de la puerta de la que se obtiene la orientación.</param>
    /// <returns>Orientación de la puerta.</returns>
    public Orientacion getOrientacionPuerta(int index) {
        return puertas[index].Item2;
    }

    /// <summary>
    /// Método que busca una puerta en la lista de puertas mediante las coordeandas, al ser un valor único, no habrá dos puertas en la misma posición.
    /// </summary>
    /// <param name="x">Coordeanda X de la puerta.</param>
    /// <param name="y">Coordeanda Y de la puerta.</param>
    /// <returns>TRUE si la puerta está en la lista; FALSE si no es así.</returns>
    public int searchDoor(int x, int y) {
        Coordenada buscada = new Coordenada(x, y);
        
        for(int i = 0; i < puertas.Count; i++) {
            if(puertas[i].Item1.compareTo(buscada)) {
                return i;
            }
        }

        return -1;
    }

    /// <summary>
    /// Método que devuelve el número de puertas.
    /// </summary>
    /// <returns>Número de puertas.</returns>
    public int getDoorsCount() {
        return puertas.Count;
    }
}
