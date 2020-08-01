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
    /// Variable que indica el tipo de la casilla.
    /// </summary>
    private Tipo_Casilla tipo;

    /// <summary>
    /// Método para inicializar los stats comunes de una casilla.
    /// </summary>
    /// <param name="x">Coordenada X de la casilla.</param>
    /// <param name="y">Coordenada Y de la casilla.</param>
    /// <param name="t">Tipo de casilla.</param>
    public void initCasilla(int x, int y, Tipo_Casilla t) {
        coordenada.setCoordenates(x, y);
        setTipo(t);
    }

    private void OnMouseUp() {
        TableroManager gridManager = GameManager.getPasillosManager();
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
    /// Getter del tipo de la casilla.
    /// </summary>
    /// <returns>Tipo de la casilla.</returns>
    public Tipo_Casilla getTipo() {
        return tipo;
    }

    /// <summary>
    /// Setter del tipo de la casilla.
    /// </summary>
    /// <param name="newTipo">Nuevo tipo de la casilla.</param>
    public void setTipo(Tipo_Casilla newTipo) {
        tipo = newTipo;
    }
}
