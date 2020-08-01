using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertaStat : CasillaStat {
    /// <summary>
    /// Orientación de la puerta.
    /// </summary>
    private Orientacion orientacion;

    /// <summary>
    /// Método para inicializar los stats de una casilla de tipo puerta.
    /// </summary>
    /// <param name="x">Coordenada X de la puerta.</param>
    /// <param name="y">Coordeanda Y de la puerta.</param>
    /// <param name="t">Tipo de casilla.</param>
    /// <param name="sentido">Orientación de la puerta.</param>
    public void initCasilla(int x, int y, Tipo_Casilla t, Orientacion sentido) {
        initCasilla(x, y, t);
        orientacion = sentido;
    }

    /// <summary>
    /// Getter de la orientación de la puerta.
    /// </summary>
    /// <returns>Orientación de la puerta.</returns>
    public Orientacion getOrientacion() {
        return orientacion;
    }

    /// <summary>
    /// Setter de la orientación de la puerta.
    /// </summary>
    /// <param name="newOrientacion">Nuevo valor de orientación de la puerta.</param>
    public void setOrientacion(Orientacion newOrientacion) {
        orientacion = newOrientacion;
    }
}
