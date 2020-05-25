using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase encargada de manejar la información relevante para el juego, como elementos únicos como
/// los mánager, el jugador actual, etc.
/// </summary>
public class GameManager : MonoBehaviour {
    /// <summary>
    /// Mánager del grid en la partida.
    /// </summary>
    private static GridManager gridManager;

    /// <summary>
    /// Jugador actual.
    /// </summary>
    private static GameObject currentPlayer;

    /// <summary>
    /// Getter del mánager del grid.
    /// </summary>
    /// <returns>Mánager del grid.</returns>
    public static GridManager getGridManager() {
        return gridManager;
    }

    /// <summary>
    /// Setter del mánager del grid.
    /// </summary>
    /// <param name="gManager">Nuevo mánager del grid.</param>
    public static void setGridManager(GridManager gManager) {
        gridManager = gManager;
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
