using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase para gestionar información de las casillas en diversos aspectos.
/// </summary>
public class CasillaStat : MonoBehaviour
{
    /// <summary>
    /// Variable que indica el número de "pasos" que se daría en una ruta concreta.
    /// </summary>
    private int visited = -1;

    /// <summary>
    /// Posiciçon en el grid.
    /// </summary>
    private Coordenada coordenada = new Coordenada();

    /// <summary>
    /// Variable booleana que indica si la casilla es una puerta.
    /// </summary>
    private bool door = false;

    private void OnMouseUp() {
        PasillosManager gridManager = GameManager.getPasillosManager();
        if (gridManager) {
            gridManager.setEndOfPath(coordenada.getX(), coordenada.getY());
            gridManager.generatePath();
        }
    }

    /// <summary>
    /// Getter de variable visited.
    /// </summary>
    /// <returns>Variable visited.</returns>
    public int getVisited() {
        return visited;
    }

    /// <summary>
    /// Setter de variable visited.
    /// </summary>
    /// <param name="newValue">Nuevo valor para la variable.</param>
    public void setVisited(int newValue) {
        visited = newValue;
    }

    /// <summary>
    /// Getter de variable x.
    /// </summary>
    /// <returns>Variable x.</returns>
    public int getX() {
        return coordenada.getX();
    }

    /// <summary>
    /// Getter de variable y.
    /// </summary>
    /// <returns>Variable y.</returns>
    public int getY() {
        return coordenada.getY();
    }

    /// <summary>
    /// Método que modifica las coordenadas en el grid del objeto.
    /// </summary>
    /// <param name="newX">Nueva coordenada x.</param>
    /// <param name="newY">Nueva coordenada y.</param>
    public void setCoordenates(int newX, int newY) {
        coordenada.setCoordenates(newX, newY);
    }

    /// <summary>
    /// Getter que nos dice si la casilla es una puerta.
    /// </summary>
    /// <returns>True si es puerta; False si no lo es.</returns>
    public bool isDoor() {
        return door;
    }

    /// <summary>
    /// Método que hace que una casilla sea además una puerta.
    /// </summary>
    public void makeDoor() {
        door = true;
    }
}
