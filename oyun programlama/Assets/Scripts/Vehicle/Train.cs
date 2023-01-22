using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening.Plugins.Core.PathCore;

public class Train : MonoBehaviour
{
    public List<Transform> roadPoints;
    public Stash stash;
    public float speed;
    private void OnEnable()
    {
        Movement();
    }
    private void Movement()
    {

        var roadPositions = new List<Vector3>();
        roadPoints.ForEach(r => roadPositions.Add(r.position));
        Path path = new (PathType.CatmullRom, roadPositions.ToArray(), 2);

        transform.DOPath(path, speed).OnWaypointChange(Rotation).SetLoops(-1).SetOptions(true);
    }
    private void Rotation(int waypoint)
    {
        Quaternion _lookRotation =
         Quaternion.LookRotation((roadPoints[waypoint].position - transform.position).normalized);

        transform.rotation = _lookRotation;

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("StartPoint"))
        {
            stash.DropAllStash();
        }
    }
}
