using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase destinada al comportamiento de la IA en cuanto al pathfinding.
/// </summary>
public class GridManager : MonoBehaviour {
    /// <summary>
    /// Filas de la matriz.
    /// </summary>
    public int rows = 24;

    /// <summary>
    /// Columnas de la matriz.
    /// </summary>
    public int columns = 22;

    /// <summary>
    /// Escala de separación entre los nodos de las casillas.
    /// </summary>
    public float scale = 0.815f;

    /// <summary>
    /// Objeto usado para detectar las posiciones en el grid.
    /// </summary>
    public GameObject gridPrefab;

    /// <summary>
    /// Posición en la que se encuentra la esquina inferior izquierda.
    /// </summary>
    public Vector2 leftBottomLocation = new Vector2(0, 0);

    /// <summary>
    /// Matriz de casillas del grid.
    /// </summary>
    private GameObject[,] gridArray;

    /// <summary>
    /// Coordenada x de la posición de comienzo del path.
    /// </summary>
    private int startX = 0;

    /// <summary>
    /// Coordenada y de la posición de comienzo del path.
    /// </summary>
    private int startY = 0;

    /// <summary>
    /// Coordenada x de la posición de fin del path.
    /// </summary>
    private int endX = 0;

    /// <summary>
    /// Coordenada y de la posición de fin del path.
    /// </summary>
    private int endY = 0;

    /// <summary>
    /// Path para ir de la posición de comienzo a la que desea el jugador.
    /// </summary>
    private List<GameObject> path = new List<GameObject>();

    void Awake() {
        gridArray = new GameObject[columns, rows];
        if (gridPrefab) {
            generateGrid();
        } else {
            Debug.LogError("Missing grid prefab, please assign.");
        }

        GameManager.setGridManager(gameObject.GetComponent<GridManager>());
    }

    /// <summary>
    /// Método que genera el grid para usar en el tablero.
    /// </summary>
    private void generateGrid() {
        for (int i = 0; i < columns; i++) {
            for (int j = 0; j < rows; j++) {
                GameObject posGrid = Instantiate(gridPrefab);
                posGrid.transform.position = new Vector2(leftBottomLocation.x + scale * i, leftBottomLocation.y + scale * j);
                posGrid.transform.SetParent(gameObject.transform);
                GridStat posGridStat = posGrid.GetComponent<GridStat>();
                posGridStat.setCoordenates(i, j);

                gridArray[i, j] = posGrid;
            }
        }
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
        int x = startX;
        int y = startY;
        int[] testArray = new int[rows * columns];

        for (int step = 1; step < testArray.Length; step++) {
            foreach (GameObject posGrid in gridArray) {
                GridStat posGridStat = posGrid.GetComponent<GridStat>();
                if (posGrid && posGridStat.getVisited() == step - 1) {
                    testFourDirections(posGridStat.getX(), posGridStat.getY(), step);
                }
            }
        }
    }

    /// <summary>
    /// Método que calcula el path del jugador para llegar a la casilla destino.
    /// </summary>
    private void setPath() {
        int step;
        int x = endX;
        int y = endY;
        List<GameObject> tempList = new List<GameObject>();
        path.Clear();

        if (gridArray[endX, endY] && gridArray[endX, endY].GetComponent<GridStat>().getVisited() > 0) {
            path.Add(gridArray[x, y]);
            step = gridArray[x, y].GetComponent<GridStat>().getVisited() - 1;
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

            GameObject tempObj = findClosest(gridArray[endX, endY].transform, tempList);
            path.Add(tempObj);
            x = tempObj.GetComponent<GridStat>().getX();
            y = tempObj.GetComponent<GridStat>().getY();
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
                posGrid.GetComponent<GridStat>().setVisited(-1);
            }
        }

        gridArray[startX, startY].GetComponent<GridStat>().setVisited(0);
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
                if (y + 1 < rows && gridArray[x, y + 1] && gridArray[x, y + 1].GetComponent<GridStat>().getVisited() == step) {
                    return true;
                } else {
                    return false;
                }

            case 2:
                if (x + 1 < columns && gridArray[x + 1, y] && gridArray[x + 1, y].GetComponent<GridStat>().getVisited() == step) {
                    return true;
                } else {
                    return false;
                }

            case 3:
                if (y - 1 > -1 && gridArray[x, y - 1] && gridArray[x, y - 1].GetComponent<GridStat>().getVisited() == step) {
                    return true;
                } else {
                    return false;
                }

            case 4:
                if (x - 1 > -1 && gridArray[x - 1, y] && gridArray[x - 1, y].GetComponent<GridStat>().getVisited() == step) {
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
            gridArray[x, y].GetComponent<GridStat>().setVisited(step);
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
        startX = x;
        startY = y;
    }

    /// <summary>
    /// Método que modifica la posición de fin del path.
    /// </summary>
    /// <param name="x">Nueva coordenada x.</param>
    /// <param name="y">Nueva coordenada y.</param>
    public void setEndOfPath(int x, int y) {
        endX = x;
        endY = y;
    }

    /// <summary>
    /// Getter de coordenada x de la casilla destino del path.
    /// </summary>
    /// <returns>Coordenada x de casilla destino del path.</returns>
    public int getEndX() {
        return endX;
    }

    /// <summary>
    /// Getter de coordenada y de la casilla destino del path.
    /// </summary>
    /// <returns>Coordenada y de casilla destino del path.</returns>
    public int getEndY() {
        return endY;
    }
}
