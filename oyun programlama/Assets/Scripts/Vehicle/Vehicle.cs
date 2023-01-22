using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vehicle : MonoBehaviour
{
    public Movement movement;
    public Collector collector;
    public Transform leavePos;
    public Button leaveButton;
    public CameraController cameraController;
    private GameObject player;

    private void Start()
    {
        movement.enabled = false;
        collector.enabled = false;
    }

    protected virtual void Leave()
    {
        movement.enabled = false;
        leaveButton.onClick.RemoveAllListeners();
        leaveButton.gameObject.SetActive(false);
        player.transform.position = new Vector3(leavePos.position.x, 0, leavePos.position.z);
        player.SetActive(true);
        cameraController.TargetTransform = player.transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        player = other.gameObject;
        leaveButton.onClick.RemoveAllListeners();
        leaveButton.onClick.AddListener(Leave);
        leaveButton.gameObject.SetActive(true);
        cameraController.TargetTransform = movement.transform;
        player.SetActive(false);
        movement.enabled = true;
        collector.enabled = true;
    }



}
