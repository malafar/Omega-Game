using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class EscenariosManager : MonoBehaviour
{
    /// <summary>
    /// Lista de los escenarios del tablero.
    /// </summary>
    private List<escenarioData> escenarios;

    /// <summary>
    /// Fichero que contiene los datos necesarios para la generación base de los escenarios.
    /// </summary>
    private string escenariosData = "Assets/Heroes_Mystery/Ficheros/escenariosData.txt";

    /// <summary>
    /// Estructura usada para la lectura y almacenamiento temporal de los datos de los escenarios antes de su generación en el tablero.
    /// </summary>
    private struct escenarioData {
        public string nombre;
        public int numEdges;
        public string[] listEdges;
    }

    /// <summary>
    /// Método que carga los datos de los escenarios desde el fichero de datos a la estructura correspondiente.
    /// </summary>
    public void loadEscenarios() {
        escenarios = new List<escenarioData>();

        // Lectura de datos
        try {
            StreamReader fichero = File.OpenText(escenariosData);
            string escenario;

            using (fichero) {
                do {
                    escenario = fichero.ReadLine();

                    if (!String.IsNullOrEmpty(escenario)) {
                        string[] stats = escenario.Split(';');

                        addEscenario(stats);
                    }
                } while (escenario != null);
            }

            fichero.Close();
        } catch (Exception ex) {
            Debug.LogError(ex.StackTrace);
        }
    }

    /// <summary>
    /// Método que añade un escenario en la lista de los datos de los escenarios.
    /// </summary>
    /// <param name="datos">Datos del escenario a añadir.</param>
    private void addEscenario(string[] datos) {
        escenarioData esc;

        esc.nombre = datos[datos.Length - 1];
        esc.numEdges = int.Parse(datos[datos.Length - 2]);
        esc.listEdges = new string[esc.numEdges];

        for(int i = 0; i < esc.numEdges; i++) {
            esc.listEdges[i] = datos[i];
        }

        escenarios.Add(esc);
    }

    /// <summary>
    /// Método que devuelve el nombre del escenario indicado por el índice.
    /// </summary>
    /// <param name="index">Índice del escenario del que se quiere obtener el nombre.</param>
    /// <returns>Nombre del escenario indicado.</returns>
    public string getNombre(int index) {
        return escenarios[index].nombre;
    }

    /// <summary>
    /// Método que devuelve el número de vértices del escenario indicado por el índice.
    /// </summary>
    /// <param name="index">Índice del escenario del que se quiere obtener el número de vertices.</param>
    /// <returns>Número de vértices del escenario indicado.</returns>
    public int getNumEdges(int index) {
        return escenarios[index].numEdges;
    }

    /// <summary>
    /// Método que devuelve la lista de vértices del escenario indicado por el índice.
    /// </summary>
    /// <param name="index">Índice del escenario del que se quiere obtener la lista de vertices.</param>
    /// <returns>Lista de vértices del escenario indicado.</returns>
    public string[] getListEdges(int index) {
        return escenarios[index].listEdges;
    }

    /// <summary>
    /// Método de consulta del número de escenarios.
    /// </summary>
    /// <returns>Número de escenarios.</returns>
    public int getEscenariosCount() {
        return escenarios.Count;
    }
}
