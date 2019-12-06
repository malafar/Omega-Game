﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase destinada al comportamiento de la IA en cuanto al pathfinding.
/// </summary>
public class GridBehaviour : MonoBehaviour
{
    public bool findDistance = false;
    public int rows = 10;
    public int columns = 10;
    public int scale = 1;
    public GameObject gridPrefab;
    public Vector2 leftBottomLocation = new Vector2(0, 0);
    public GameObject[,] gridArray;
    public int startX = 0;
    public int startY = 0;
    public int endX = 2;
    public int endY = 2;
    public List<GameObject> path = new List<GameObject>();
    
    void Awake(){

        gridArray = new GameObject[columns, rows];
        if (gridPrefab) {
            generateGrid();
        } else {
            Debug.LogError("Missing grid prefab, please assign.");
        }
    }

    void Update(){
        if (findDistance) {
            setDistance();
            setPath();
            findDistance = false;
        }
    }

    private void generateGrid() {
        for(int i = 0; i < columns; i++) {
            for(int j = 0; j < rows; j++) {
                GameObject obj = Instantiate(gridPrefab);
                obj.transform.position = new Vector2(leftBottomLocation.x + scale * i, leftBottomLocation.y + scale * j);
                obj.transform.SetParent(gameObject.transform);
                obj.GetComponent<GridStat>().x = i;
                obj.GetComponent<GridStat>().y = j;

                gridArray[i, j] = obj;
            }
        }
    }

    private void setDistance() {
        initialSetUp();
        int x = startX;
        int y = startY;
        int[] testArray = new int[rows * columns];

        for(int step = 1; step < testArray.Length; step++) {
            foreach(GameObject obj in gridArray) {
                if(obj && obj.GetComponent<GridStat>().visited == step - 1) {
                    testFourDirections(obj.GetComponent<GridStat>().x, obj.GetComponent<GridStat>().y, step);
                }
            }
        }
    }

    private void setPath() {
        int step;
        int x = endX;
        int y = endY;
        List<GameObject> tempList = new List<GameObject>();
        path.Clear();

        if(gridArray[endX, endY] && gridArray[endX, endY].GetComponent<GridStat>().visited > 0) {
            path.Add(gridArray[x, y]);
            step = gridArray[x, y].GetComponent<GridStat>().visited - 1;
        } else {
            Debug.LogError("Can't reach the desired location");
            return;
        }

        for(int i = step; step > -1; step--) {
            if(testDirection(x, y, step, 1)) {
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
            x = tempObj.GetComponent<GridStat>().x;
            y = tempObj.GetComponent<GridStat>().y;
            tempList.Clear();
        }

    }

    private void initialSetUp() {
        foreach(GameObject obj in gridArray) {
            if (obj) {
                obj.GetComponent<GridStat>().visited = -1;
            }
        }

        gridArray[startX, startY].GetComponent<GridStat>().visited = 0;
    }

    private bool testDirection(int x, int y, int step, int direction) {
        // int directions tells which case to use 1 is up, 2 is right, 3 is down, 4 is left
        switch (direction) {
            case 1:
                if(y + 1 < rows && gridArray[x, y + 1] && gridArray[x, y + 1].GetComponent<GridStat>().visited == step) {
                    return true;
                } else {
                    return false;
                }

            case 2:
                if (x + 1 < columns && gridArray[x + 1, y] && gridArray[x + 1, y].GetComponent<GridStat>().visited == step) {
                    return true;
                } else {
                    return false;
                }

            case 3:
                if (y - 1 > -1 && gridArray[x, y - 1] && gridArray[x, y - 1].GetComponent<GridStat>().visited == step) {
                    return true;
                } else {
                    return false;
                }

            case 4:
                if (x - 1 > -1 && gridArray[x - 1, y] && gridArray[x - 1, y].GetComponent<GridStat>().visited == step) {
                    return true;
                } else {
                    return false;
                }
        }

        return false;
    }

    private void testFourDirections(int x, int y, int step) {
        if(testDirection(x, y, -1, 1)) {
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

    private void setVisited(int x, int y, int step) {
        if(gridArray[x, y]) {
            gridArray[x, y].GetComponent<GridStat>().visited = step;
        }
    }

    private GameObject findClosest(Transform targetLocation, List<GameObject> list) {
        float currentDistance = scale * rows * columns;
        int indexNumber = 0;

        for(int i = 0; i < list.Count; i++) {
            if(Vector2.Distance(targetLocation.position, list[i].transform.position) < currentDistance) {
                currentDistance = Vector2.Distance(targetLocation.position, list[i].transform.position);
                indexNumber = i;
            }
        }

        return list[indexNumber];
    }
}
