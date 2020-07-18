using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase encargada de la mecánica del seguimiento de caminos de los jugadores.
/// </summary>
public class FollowPath : MonoBehaviour {

    /// <summary>
    /// Variable para indicar si se quiere situar en la variable de inicio. TEMPORAL, para comenzar las pruebas hasta que se tenga la salida oficial.
    /// </summary>
    public bool goToStarPosition = false;

    /// <summary>
    /// Tiempo para pasar de una casilla a otra.
    /// </summary>
    public float timeForStep = 0.3f;

    /// <summary>
    /// Coordenada de comienzo para
    /// </summary>
    private Coordenada currentCoordenate = new Coordenada(4, 6); // TODO: provisional hasta que no se tenga ajustado el tema comienzo, cuando sed tenga, cambiar a constructor por defecto

    void Update() {
        if (goToStarPosition) {
            goToStarPosition = false;
            PasillosManager gridManager = GameManager.getPasillosManager();
            if (gridManager.getGridPosition(currentCoordenate.getX(), currentCoordenate.getY())) {
                transform.position = gridManager.getGridPosition(currentCoordenate.getX(), currentCoordenate.getY()).transform.position;
                gridManager.setStartOfPath(currentCoordenate.getX(), currentCoordenate.getY());
            }
        }
    }

    /// <summary>
    /// Método que hace que se siga el path calculado. También cambia la posición de inicio para
    /// la siguiente iteración.
    /// </summary>
    public void followPath() {
        if (gameObject.GetComponent<Player>().isCurrentPlayer()) {
            PasillosManager gridManager = GameManager.getPasillosManager();
            int pathTam = gridManager.getPathCount();
            Vector3[] tempPath = new Vector3[pathTam];
            for (int i = 0; i < pathTam; i++) {
                tempPath[i] = gridManager.getPathPosition(i).transform.position;
            }
            transform.DOPath(tempPath, pathTam * timeForStep);

            currentCoordenate.setX(gridManager.getEndX());
            currentCoordenate.setY(gridManager.getEndY());

            gridManager.setStartOfPath(currentCoordenate.getX(), currentCoordenate.getY());
        }
    }
}
