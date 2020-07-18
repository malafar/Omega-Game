using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class PuertasManager : SerializedMonoBehaviour {
    /// <summary>
    /// Estructura usada para hacer más eficiente la asignación de las coordenadas de las puertas en el editor.
    /// </summary>
    public struct CoordenadaPuerta {
        public int x;
        public int y;
    }

    /// <summary>
    /// Lista de puertas del tablero.
    /// </summary>
    [Title("Ubicaciones de puertas")]
    [ListDrawerSettings(ShowIndexLabels = true)]
    public List<CoordenadaPuerta> puertas;

    /// <summary>
    /// Método que configura asigna las puertas correspondientes en las casillas pasillo.
    /// </summary>
    public void configPuertas() {
        PasillosManager pasillos = GameManager.getPasillosManager();

        puertas.ForEach(p => {
            if (pasillos.casillasPasillo[p.x, p.y]) {
                pasillos.getGridPosition(p.x, p.y).GetComponent<CasillaStat>().makeDoor();
            }
        });
    }
}
