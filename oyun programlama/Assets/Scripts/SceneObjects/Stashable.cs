using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stashable : MonoBehaviour
{
    public void CollectStashable(Transform stashParent, float yLocalPosition, Action onCompleteCollect = null)
    {
        var completionRadius = .5f;
        var speed = 150f;
        var targetPos = stashParent.position + Vector3.up * yLocalPosition;
        Tweener tweener = transform.DOMove(targetPos, speed).SetSpeedBased(true);
        tweener.OnUpdate(delegate () {
            transform.LookAt(stashParent, Vector3.up);

            if (Vector3.Distance(transform.position, targetPos) > completionRadius)
            {
                targetPos = stashParent.position + Vector3.up * yLocalPosition; 
                tweener.ChangeEndValue(targetPos, true);
            }

        }).OnComplete(() => {
            transform.parent = stashParent;
            transform.localPosition = Vector3.up * yLocalPosition;
            transform.localRotation = Quaternion.identity;
            onCompleteCollect?.Invoke();
        });
    }
    public void PayStashable(Transform target, Action onCompletePay)
    {
        transform.parent = null;

        Vector3 targetPos = target.position;
        Vector3 direction = targetPos - transform.position;
        direction.y = 0;

        var middlePos = transform.position + direction / 2f;
        middlePos.y = transform.position.y + 2f;
        var duration = 0.3f;

        transform.DOPath(new [] { middlePos, targetPos }, duration, PathType.CatmullRom)
                    .OnComplete(() =>
                    {
                        onCompletePay?.Invoke();
                        Destroy(gameObject);
                    }); 

    }

}
