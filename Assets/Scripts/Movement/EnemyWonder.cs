using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWonder : BaseEnemyMovement
{
    [SerializeField]
    Transform player;
    Vector3 directionVector;
    [SerializeField]
    LineRenderer line;
    [SerializeField]
    float wonderingTime;
    Vector3[] path;
    [SerializeField]
    PathType pathType;

    public void SetWonderingPath(LineRenderer line)
    {
        path = new Vector3[line.positionCount];
        for (int i = 0; i < path.Length; i++)
        {
            path[i] = line.GetPosition(i);
        }
        transform.position = path[0];
        gameObject.SetActive(true);
        player = ServiceLocator.GetService<IPlayerSpawner>().player.transform;
        transform.DOPath(path, 6, PathType.CatmullRom).OnComplete(()=> { gameObject.SetActive(false); });
    }

    // Update is called once per frame
    void Update()
    {
        directionVector = (player.position - transform.position).normalized;
        float angle = Mathf.Atan2(directionVector.y, directionVector.x) * Mathf.Rad2Deg;
        Quaternion directionAngle = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, directionAngle, speed * Time.deltaTime);
    }
}
