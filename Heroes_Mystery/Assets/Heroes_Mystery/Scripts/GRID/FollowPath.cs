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
    /// Coordenada x de la posición en la que se encuentra.
    /// </summary>
    public int posStartX = 0;

    /// <summary>
    /// Coordenada y de la posición en la que se encuentra.
    /// </summary>
    public int posStartY = 0;

    void Update() {
        if (goToStarPosition) {
            goToStarPosition = false;
            GridManager gridManager = GameManager.getGridManager();
            if (gridManager.getGridPosition(posStartX, posStartY)) {
                transform.position = gridManager.getGridPosition(posStartX, posStartY).transform.position;
                gridManager.setStartOfPath(posStartX, posStartY);
            }
        }
    }

    /// <summary>
    /// Método que hace que se siga el path calculado. También cambia la posición de inicio para
    /// la siguiente iteración.
    /// </summary>
    public void followPath() {
        if (gameObject.GetComponent<Player>().isCurrentPlayer()) {
            GridManager gridManager = GameManager.getGridManager();
            int pathTam = gridManager.getPathCount();
            Vector3[] tempPath = new Vector3[pathTam];
            for (int i = 0; i < pathTam; i++) {
                tempPath[i] = gridManager.getPathPosition(i).transform.position;
            }
            transform.DOPath(tempPath, pathTam * timeForStep);

            posStartX = gridManager.getEndX();
            posStartY = gridManager.getEndY();

            gridManager.setStartOfPath(posStartX, posStartY);
        }
    }
}
