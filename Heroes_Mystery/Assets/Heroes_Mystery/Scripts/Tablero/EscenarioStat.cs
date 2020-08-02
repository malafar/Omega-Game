using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscenarioStat : MonoBehaviour
{
    /// <summary>
    /// Nombre que identifica al escenario.
    /// </summary>
    private Nombre_Escenario nombre;

    /// <summary>
    /// Collider del escenario.
    /// </summary>
    private PolygonCollider2D colliderEscenario;

    /// <summary>
    /// Número de vertices del collider.
    /// </summary>
    private int edgeCollider;

    /// <summary>
    /// Método usado para inicializar el escenario.
    /// </summary>
    /// <param name="nombreEscenario">Nombre del escenario.</param>
    /// <param name="numEdges">Número de vértices del collider del escenario.</param>
    /// <param name="edges">Coordenadas de los vértices del collider.</param>
    public void initEscenario(string nombreEscenario, int numEdges, string[] edges) {
        nombre = parseNombre(nombreEscenario);
        edgeCollider = numEdges;
        gameObject.AddComponent<PolygonCollider2D>();
        colliderEscenario = gameObject.GetComponent<PolygonCollider2D>();
        fixEdges(edges);
    }

    /// <summary>
    /// Método para consultar el nombre del escenario.
    /// </summary>
    /// <returns>Nombre del escenario.</returns>
    public Nombre_Escenario getNombre() {
        return nombre;
    }

    /// <summary>
    /// Método que parsea el nombre del escenario desde una cadena de caracteres.
    /// </summary>
    /// <param name="nom">Cadena de caracteres que identifica el nombre.</param>
    /// <returns>Nombre parseado.</returns>
    private Nombre_Escenario parseNombre(string nom) {
        switch (nom) {
            case "MAZ":
                return Nombre_Escenario.MAZMORRA;

            case "APO":
                return Nombre_Escenario.APOSENTOS;

            case "COC":
                return Nombre_Escenario.COCINA;

            case "EST":
                return Nombre_Escenario.ESTABLO;

            case "TOR":
                return Nombre_Escenario.TORRE;

            case "CAM":
                return Nombre_Escenario.CAMPO_BATALLA;

            case "CAS":
                return Nombre_Escenario.CASCADA;

            case "CAP":
                return Nombre_Escenario.CAPILLA;

            case "BIB":
                return Nombre_Escenario.BIBLIOTECA;

            case "COM":
                return Nombre_Escenario.COMEDOR;

            case "CUA":
                return Nombre_Escenario.CUARTO_APRENDIZAJE;

            default:
                return Nombre_Escenario.NONE;
        }
    }

    /// <summary>
    /// Método que configura los vértices del collider del escenario.
    /// </summary>
    /// <param name="edges">Lista con los datos de los vértices.</param>
    private void fixEdges(string[] edges) {

    }
}
