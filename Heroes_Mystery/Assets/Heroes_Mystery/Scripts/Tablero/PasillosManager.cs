using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

/// <summary>
/// Clase destinada al comportamiento de la IA en cuanto al pathfinding.
/// </summary>
public class PasillosManager : SerializedMonoBehaviour {
    /// <summary>
    /// Matriz booleana para indicar casillas de tipo pasillo.
    /// </summary>
	[TableMatrix(HorizontalTitle = "Casillas Pasillo", SquareCells = true)]
	public bool[,] casillasPasillo = new bool[60,24];

    /// <summary>
    /// Objeto usado para detectar las posiciones en el grid.
    /// </summary>
    public GameObject gridPrefab;
	
	/// <summary>
    /// Filas de la matriz.
    /// </summary>
    private int rows = 24;

    /// <summary>
    /// Columnas de la matriz.
    /// </summary>
    private int columns = 60;

    /// <summary>
    /// Escala de separación entre los nodos de las casillas.
    /// </summary>
    private float scale = 0.3f;

    /// <summary>
    /// Posición en la que se encuentra la esquina superior izquierda.
    /// </summary>
    private Vector2 leftUpLocation = new Vector2(0, 0);

    /// <summary>
    /// Matriz de casillas del grid.
    /// </summary>
    private GameObject[,] gridArray;

    /// <summary>
    /// Coordenadas de la posición de comienzo del path.
    /// </summary>
    private Coordenada startCoordenate = new Coordenada();

    /// <summary>
    /// Coordeandas de la posición de fin del path.
    /// </summary>
    private Coordenada endCoordenate = new Coordenada();

    /// <summary>
    /// Path para ir de la posición de comienzo a la que desea el jugador.
    /// </summary>
    private List<GameObject> path = new List<GameObject>();

    /// <summary>
    /// Elemento al que se llamará para configurar las puertas una vez se creen los pasillos.
    /// </summary>
    private PuertasManager puertasManager;

    /// <summary>
    /// Método llamado una vez configurados los manager para generar el tablero.
    /// </summary>
    public void generateTablero() {
        gridArray = new GameObject[columns, rows];
        if (gridPrefab) {
            generatePasillos();
        } else {
            Debug.LogError("Falta añadir un prefab para las casillas.");
        }
    }

    /// <summary>
    /// Método que genera el grid para usar en el tablero.
    /// </summary>
    private void generatePasillos() {
        for (int i = 0; i < columns; i++) {
            for (int j = 0; j < rows; j++) {
                if(casillasPasillo[i, j]){
					GameObject posCasilla = Instantiate(gridPrefab);
					posCasilla.transform.position = new Vector2(leftUpLocation.x + scale * i, leftUpLocation.y - scale * j);
					posCasilla.transform.SetParent(gameObject.transform);
					CasillaStat posGridStat = posCasilla.GetComponent<CasillaStat>();
					posGridStat.setCoordenates(i, j);

					gridArray[i, j] = posCasilla;	
				}
            }
        }

        puertasManager.configPuertas();
    }

    /// <summary>
    /// Método al que se llama para generar el path de la ruta que desea el jugador.
    /// Tras generarlo, hace que el jugador siga dicha ruta.
    /// </summary>
    public void generatePath() {
        setDistance();
        setPath();
        GameManager.getCurrentPlayer().GetComponent<FollowPath>().followPath();
    }

    /// <summary>
    /// Método que configura las distancias de las casillas del grid a la hora de calcular
    /// la ruta a seguir por el jugador hasta la casilla que desea.
    /// </summary>
    private void setDistance() {
        initialSetUp();
        int x = startCoordenate.getX();
        int y = startCoordenate.getY();
        int[] testArray = new int[rows * columns];

        for (int step = 1; step < testArray.Length; step++) {
            foreach (GameObject posGrid in gridArray) {
                if(posGrid){
					CasillaStat posGridStat = posGrid.GetComponent<CasillaStat>();
					if (posGrid && posGridStat.getVisited() == step - 1) {
						testFourDirections(posGridStat.getX(), posGridStat.getY(), step);
					}
				}
            }
        }
    }

    /// <summary>
    /// Método que calcula el path del jugador para llegar a la casilla destino.
    /// </summary>
    private void setPath() {
        int step;
        int x = endCoordenate.getX();
        int y = endCoordenate.getY();
        List<GameObject> tempList = new List<GameObject>();
        path.Clear();

        if (gridArray[endCoordenate.getX(), endCoordenate.getY()] && gridArray[endCoordenate.getX(), endCoordenate.getY()].GetComponent<CasillaStat>().getVisited() > 0) {
            path.Add(gridArray[x, y]);
            step = gridArray[x, y].GetComponent<CasillaStat>().getVisited() - 1;
        } else {
            Debug.LogError("Can't reach the desired location");
            return;
        }

        for (int i = step; step > -1; step--) {
            if (testDirection(x, y, step, 1)) {
                tempList.Add(gridArray[x, y + 1]);
            }

            if (testDirection(x, y, step, 2)) {
                tempList.Add(gridArray[x + 1, y]);
            }

            if (testDirection(x, y, step, 3)) {
                tempList.Add(gridArray[x, y - 1]);
            }

            if (testDirection(x, y, step, 4)) {
                tempList.Add(gridArray[x - 1, y]);
            }

            GameObject tempObj = findClosest(gridArray[endCoordenate.getX(), endCoordenate.getY()].transform, tempList);
            path.Add(tempObj);
            x = tempObj.GetComponent<CasillaStat>().getX();
            y = tempObj.GetComponent<CasillaStat>().getY();
            tempList.Clear();
        }

        // Necesario el Reverse ya que el path se genera calculando desde la casilla destino.
        path.Reverse();
    }

    /// <summary>
    /// Método que realiza la configuración inicial en las casillas, exectuando sus posiciones.
    /// </summary>
    private void initialSetUp() {
        foreach (GameObject posGrid in gridArray) {
            if (posGrid) {
                posGrid.GetComponent<CasillaStat>().setVisited(-1);
            }
        }

        gridArray[startCoordenate.getX(), startCoordenate.getY()].GetComponent<CasillaStat>().setVisited(0);
    }

    /// <summary>
    /// Método que calcula si una dirección puede ser tomada en consideración o no.
    /// </summary>
    /// <param name="x">Coordenada x de la casilla desde la que se analiza.</param>
    /// <param name="y">Coordenada y de la casilla desde la que se analiza.</param>
    /// <param name="step">Número de paso que se busca en la casilla.</param>
    /// <param name="direction">Dirección que se comprueba: 1 (arriba), 2 (dcha), 3 (abajo), 4 (izqda).</param>
    /// <returns>TRUE si se puede tomar en consideración; FALSE en caso contrario.</returns>
    private bool testDirection(int x, int y, int step, int direction) {
        switch (direction) {
            case 1:
                if (y + 1 < rows && gridArray[x, y + 1] && gridArray[x, y + 1].GetComponent<CasillaStat>().getVisited() == step) {
                    return true;
                } else {
                    return false;
                }

            case 2:
                if (x + 1 < columns && gridArray[x + 1, y] && gridArray[x + 1, y].GetComponent<CasillaStat>().getVisited() == step) {
                    return true;
                } else {
                    return false;
                }

            case 3:
                if (y - 1 > -1 && gridArray[x, y - 1] && gridArray[x, y - 1].GetComponent<CasillaStat>().getVisited() == step) {
                    return true;
                } else {
                    return false;
                }

            case 4:
                if (x - 1 > -1 && gridArray[x - 1, y] && gridArray[x - 1, y].GetComponent<CasillaStat>().getVisited() == step) {
                    return true;
                } else {
                    return false;
                }
        }

        return false;
    }

    /// <summary>
    /// Método que comprueba las cuatro direcciones desde una casilla.
    /// </summary>
    /// <param name="x">Coordenada x de la casilla desde la que se analiza.</param>
    /// <param name="y">Coordenada y de la casilla desde la que se analiza.</param>
    /// <param name="step">Número de paso que se busca en la casilla.</param>
    private void testFourDirections(int x, int y, int step) {
        if (testDirection(x, y, -1, 1)) {
            setVisited(x, y + 1, step);
        }

        if (testDirection(x, y, -1, 2)) {
            setVisited(x + 1, y, step);
        }

        if (testDirection(x, y, -1, 3)) {
            setVisited(x, y - 1, step);
        }

        if (testDirection(x, y, -1, 4)) {
            setVisited(x - 1, y, step);
        }
    }

    /// <summary>
    /// Método que modifica el número de paso en la ruta de una casilla.
    /// </summary>
    /// <param name="x">Coordenada x de la casilla que se modificará.</param>
    /// <param name="y">Coordenada y de la casilla que se modificará.</param>
    /// <param name="step">Número de paso a asignar.</param>
    private void setVisited(int x, int y, int step) {
        if (gridArray[x, y]) {
            gridArray[x, y].GetComponent<CasillaStat>().setVisited(step);
        }
    }

    /// <summary>
    /// Método que encuentra la casilla más cerca en una lista hasta la posición de destino de la ruta.
    /// </summary>
    /// <param name="targetLocation">Posición destino de la ruta.</param>
    /// <param name="list">Lista con las casillas a analizar.</param>
    /// <returns>Casilla más cercana en la lista a la casilla destino.</returns>
    private GameObject findClosest(Transform targetLocation, List<GameObject> list) {
        float currentDistance = scale * rows * columns;
        int indexNumber = 0;

        for (int i = 0; i < list.Count; i++) {
            if (Vector2.Distance(targetLocation.position, list[i].transform.position) < currentDistance) {
                currentDistance = Vector2.Distance(targetLocation.position, list[i].transform.position);
                indexNumber = i;
            }
        }

        return list[indexNumber];
    }

    /// <summary>
    /// Método que devuelve el número de elementos en el path.
    /// </summary>
    /// <returns>Número de elementos en el path.</returns>
    public int getPathCount() {
        return path.Count;
    }

    /// <summary>
    /// Método que devuelve una posición concreta del path.
    /// </summary>
    /// <param name="index">Índice de la posición del path deseada.</param>
    /// <returns>Posición del path.</returns>
    public GameObject getPathPosition(int index) {
        return path[index];
    }

    /// <summary>
    /// Método que devuelve una posición concreta del grid.
    /// </summary>
    /// <param name="x">Coordenada x de la posición deseada.</param>
    /// <param name="y">Coordenada y de la posición deseada.</param>
    /// <returns>Posición del grid.</returns>
    public GameObject getGridPosition(int x, int y) {
        return gridArray[x, y];
    }

    /// <summary>
    /// Método que modifica la posición de inicio del path.
    /// </summary>
    /// <param name="x">Nueva coordenada x.</param>
    /// <param name="y">Nueva coordenada y.</param>
    public void setStartOfPath(int x, int y) {
        startCoordenate.setCoordenates(x, y);
    }

    /// <summary>
    /// Método que modifica la posición de fin del path.
    /// </summary>
    /// <param name="x">Nueva coordenada x.</param>
    /// <param name="y">Nueva coordenada y.</param>
    public void setEndOfPath(int x, int y) {
        endCoordenate.setCoordenates(x, y);
    }

    /// <summary>
    /// Getter de coordenada x de la casilla destino del path.
    /// </summary>
    /// <returns>Coordenada x de casilla destino del path.</returns>
    public int getEndX() {
        return endCoordenate.getX();
    }

    /// <summary>
    /// Getter de coordenada y de la casilla destino del path.
    /// </summary>
    /// <returns>Coordenada y de casilla destino del path.</returns>
    public int getEndY() {
        return endCoordenate.getY();
    }

    public void setPuertasManager(PuertasManager pManager) {
        puertasManager = pManager;
    }
}
