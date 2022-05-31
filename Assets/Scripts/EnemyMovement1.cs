using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;
public class EnemyMovement1 : MonoBehaviour
{
    public LineRenderer LineRenderer;
    public Vector3[] pathTransform;
    public List<Transform> enemies;
    public PathType pathType = PathType.Linear;
    Vector3[] path;
    bool done = false;
    int index=0;
    private void OnEnable()
    {
        path = new Vector3[LineRenderer.positionCount];
        for (int i = 0; i < path.Length; i++)
        {
            path[i] = LineRenderer.GetPosition(i);
        }
        transform.position = path[0];
        MoveEnemies();
    }
    void MoveEnemies()
    {

            transform.DOPath(path, 4, pathType,PathMode.TopDown2D).SetLookAt(path[index],true).OnWaypointChange(OnCOmplete).OnComplete(()=> { done = true; });
    }
    private void OnCOmplete(int i)
    {
        index = i;
    }
}
