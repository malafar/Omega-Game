using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase encargada de manejar la información relevante para el juego, como elementos únicos como
/// los mánager, el jugador actual, etc.
/// </summary>
public class GameManager : MonoBehaviour {
    /// <summary>
    /// Mánager del tablero en la partida.
    /// </summary>
    private static GameObject tableroManager;

    /// <summary>
    /// Jugador actual.
    /// </summary>
    private static GameObject currentPlayer;

    /// <summary>
    /// Getter del mánager del tablero.
    /// </summary>
    /// <returns>Mánager del tablero.</returns>
    public static GameObject getTableroManager() {
        return tableroManager;
    }

    /// <summary>
    /// Setter del mánager del tablero.
    /// </summary>
    /// <param name="gManager">Nuevo mánager del tablero.</param>
    public static void setTableroManager(GameObject tManager) {
        tableroManager = tManager;

        if (tableroManager) {
            TableroManager pManager = getPasillosManager();
            pManager.setPuertasManager(getPuertasManager());
            pManager.setEscenariosManager(getEscenariosManager());
            pManager.generateTablero();
        }
    }

    /// <summary>
    /// Getter del mánager del pasillo.
    /// </summary>
    /// <returns>Mánager del pasillo.</returns>
    public static TableroManager getPasillosManager() {
        return tableroManager.GetComponent<TableroManager>();
    }

    /// <summary>
    /// Getter del mánager de las puertas.
    /// </summary>
    /// <returns>Mánager de las puertas.</returns>
    public static PuertasManager getPuertasManager() {
        return tableroManager.GetComponent<PuertasManager>();
    }

    /// <summary>
    /// Getter del mánager de los escenarios.
    /// </summary>
    /// <returns>Mánager de los escenarios.</returns>
    public static EscenariosManager getEscenariosManager() {
        return tableroManager.GetComponent<EscenariosManager>();
    }

    /// <summary>
    /// Getter del jugador actual.
    /// </summary>
    /// <returns>Jugador actual.</returns>
    public static GameObject getCurrentPlayer() {
        return currentPlayer;
    }

    /// <summary>
    /// Setter del jugador actual.
    /// </summary>
    /// <param name="player">Nuevo jugador actual.</param>
    public static void setCurrentPlayer(GameObject player) {
        if (currentPlayer) {
            currentPlayer.GetComponent<Player>().changeIsCurrentPlayer();
        }

        currentPlayer = player;
        currentPlayer.GetComponent<Player>().changeIsCurrentPlayer();
    }
}
