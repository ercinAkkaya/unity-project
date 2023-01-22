using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yatch : Vehicle
{
    [SerializeField] private Stash stash;

    private Vector3 startPos;
    private Quaternion startRot;
    private void Start()
    {
        movement.enabled = false;
        collector.enabled = false;
        startPos = movement.transform.position;
        startRot = movement.transform.rotation;
    }

    protected override void Leave()
    {
        base.Leave();
        movement.transform.position = startPos;
        movement.transform.rotation = startRot;
        stash.DropAllStash();
    }
}
