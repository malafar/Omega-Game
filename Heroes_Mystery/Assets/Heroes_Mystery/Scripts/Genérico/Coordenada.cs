using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Coordenada {
    /// <summary>
    /// Coordenada en X.
    /// </summary>
    private int x;

    /// <summary>
    /// Coordenada en Y.
    /// </summary>
    private int y;

    /// <summary>
    /// Constructor por defecto de una coordeanda, que la coloca en el punto (0,0).
    /// </summary>
    public Coordenada() {
        x = 0;
        y = 0;
    }

    /// <summary>
    /// Constructor por parámetros para una coordenada.
    /// </summary>
    /// <param name="valorX">Valor a asignar a X.</param>
    /// <param name="valorY">Valor a asignar a Y.</param>
    public Coordenada(int valorX, int valorY) {
        x = valorX;
        y = valorY;
    }

    /// <summary>
    /// Método que devuelve la coordenada en el eje X.
    /// </summary>
    /// <returns>Coordenada en el eje X.</returns>
    public int getX() {
        return x;
    }

    /// <summary>
    /// Método para cambiar la coordenada en el eje X.
    /// </summary>
    /// <param name="newX">Nuevo valor para el eje X.</param>
    public void setX(int newX) {
        x = newX;
    }

    /// <summary>
    /// Método que devuelve la coordenada en el eje Y.
    /// </summary>
    /// <returns>Coordenada en el eje Y.</returns>
    public int getY() {
        return y;
    }

    /// <summary>
    /// Método para cambiar la coordenada en el eje Y.
    /// </summary>
    /// <param name="newY">Nuevo valor para el eje Y.</param>
    public void setY(int newY) {
        y = newY;
    }

    /// <summary>
    /// Método encargado de la asignación de las coordenadas de la puerta.
    /// </summary>
    /// <param name="newX">Nuevo valor para el eje X.</param>
    /// <param name="newY">Nuevo valor para el eje Y.</param>
    public void setCoordenates(int newX, int newY) {
        x = newX;
        y = newY;
    }

    /// <summary>
    /// Método que compara la coordenada actual con otra.
    /// </summary>
    /// <param name="other">Coordenada con la que se compara.</param>
    /// <returns>TRUE en caso de que sean la misma; FALSE en caso contrario.</returns>
    public bool compareTo(Coordenada other) {
        return x == other.getX() && y == other.getY();
    }
}
