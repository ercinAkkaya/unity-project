using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stash : MonoBehaviour
{
    public Collectable collectablePrefab;
    public Transform collectableDropPosition;
    public Transform stashParent;
    public List<Stashable> CollectedObjects = new ();

    public int CollectedCount => CollectedObjects.Count;
    public float collectionHeight = 1;
    public int maxCollectableCount = 5;

    public void AddStash(Collectable collectedObject)
    {
        if (CollectedCount >= maxCollectableCount)
            return;

        var yLocalPosition = CollectedCount * collectionHeight;

        Stashable stashable = collectedObject.Collect(); 
        stashable.CollectStashable(stashParent, yLocalPosition);
        CollectedObjects.Add(stashable);
         
    }

    public Stashable RemoveStash()
    {
        if (CollectedCount <= 0)
            return null;

        var stashable = CollectedObjects[CollectedCount - 1];

        CollectedObjects.Remove(stashable);
        stashable.transform.parent = null;

        return stashable;

    }
    public void DropAllStash()
    {
        for (int i = CollectedCount - 1; i >= 0; i--)
        {
            Stashable removed = RemoveStash();
            Destroy(removed.gameObject);
            Collectable collectable = Instantiate(collectablePrefab, null);
            collectable.transform.position = stashParent.transform.position;
            collectable.transform.localScale = Vector3.zero;
            collectable.transform.DOMove(collectableDropPosition.position, .5f);
            collectable.transform.DOScale(1f, 0.5f).SetEase(Ease.OutBack, 2.5f);
            collectable.transform.DORotate(Vector3.up * 360f, 5f, RotateMode.LocalAxisAdd).SetLoops(-1);
        }

    }

}
