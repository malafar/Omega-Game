using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase encargada de la información de los jugadores.
/// </summary>
public class Player : MonoBehaviour
{
    /// <summary>
    /// Bool que indica si es el jugador actual o no.
    /// </summary>
    private bool currentPlayer = false;

    private void Awake() {
        GameManager.setCurrentPlayer(gameObject);
    }// PROVISIONAL, hasta que la estructura final esté finalizada

    /// <summary>
    /// Getter de si el jugador es el actual o no.
    /// </summary>
    /// <returns>Bool que indica si es el jugador actual o no.</returns>
    public bool isCurrentPlayer() {
        return currentPlayer;
    }

    /// <summary>
    /// Método para cambiar el valor de si el jugador es el jugador actual o no.
    /// </summary>
    public void changeIsCurrentPlayer() {
        currentPlayer = !currentPlayer;
    }
}
