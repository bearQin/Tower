using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPoints : MonoBehaviour
{
    public static Transform[] pathPoints;
    private void Awake()
    {
        pathPoints = new Transform[transform.childCount];
        for(int i = 0; i < pathPoints.Length; i++)
        {
            pathPoints[i] = transform.GetChild(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
