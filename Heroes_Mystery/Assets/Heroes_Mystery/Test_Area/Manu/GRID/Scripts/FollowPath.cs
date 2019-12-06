using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    public bool startPosition = false;
    public bool followingActivated = false;
    public GridBehaviour gridManager;
    // Start is called before the first frame update
    void Start()
    {
        if (gridManager.path.Count > 0) {
            transform.position = gridManager.path[0].transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (startPosition) {
            if (gridManager.path.Count > 0) {
                transform.position = gridManager.path[0].transform.position;
            }
            startPosition = false;
        }

        if (followingActivated) {
            followingActivated = false;
            Vector3[] tempPath = new Vector3[gridManager.path.Count];
            for(int i = 0; i < gridManager.path.Count; i++) {
                tempPath[i] = gridManager.path[i].transform.position;            
            }
            transform.DOPath(tempPath, 4);
        }
    }
}
