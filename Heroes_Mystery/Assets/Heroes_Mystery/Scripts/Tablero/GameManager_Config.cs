using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_Config : MonoBehaviour
{
    /// <summary>
    /// Objeto que realiza la configuración del tablero del juego.
    /// </summary>
    public GameObject tableroManager;

    private void Awake() {
        GameManager.setTableroManager(tableroManager);
    }
}
