using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{ 
    public VariableJoystick joystick;
    public Animator animCtrl;
    public float minX, maxX, minZ, maxZ;
    public float Speed = 5f; 
    public float RotationSpeed = 10f;
    private bool _isjoystickNull;
    private bool _isanimCtrlNull;

    private void Start()
    {
        _isanimCtrlNull = animCtrl == null;
        _isjoystickNull = joystick == null;
    }

    void Update()
    {
        if (_isjoystickNull)
            return;

        Vector2 direction = joystick.Direction;

        Vector3 movementVector = new (direction.x, 0, direction.y);

        movementVector *= (Time.deltaTime * Speed);

        transform.position += movementVector;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX),
            transform.position.y,
            Mathf.Clamp(transform.position.z, minZ, maxZ));

        if (movementVector.magnitude != 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation,  Quaternion.LookRotation(movementVector, Vector3.up), Time.deltaTime * RotationSpeed);
        }

        bool isWalking = direction.magnitude > 0;
        if (_isanimCtrlNull)
            return;
        animCtrl.SetBool("IsWalking", isWalking);

        animCtrl.SetFloat("SpeedValue", direction.magnitude);
         
    }

}
