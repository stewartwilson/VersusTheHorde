using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MasterPlayerMotor))]
public class MasterPlayerController : MonoBehaviour {

    [SerializeField]
    private float movementSpeed = 5f;
    [SerializeField]
    private float lookSensitivity = 1.0f;

    [SerializeField]
    private float maxSpawnDistance = 999f;

    private MasterPlayerMotor motor;

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private GameObject cursor;

    [SerializeField]
    private LayerMask groundLayer;

    private void Start()
    {
        motor = GetComponent<MasterPlayerMotor>();
    }

    private void Update()
    {
        float _xMovement = Input.GetAxisRaw("Horizontal");
        float _yMovement = Input.GetAxisRaw("Vertical");

        Vector3 _moveHorizontal = transform.right * _xMovement;
        Vector3 _moveVertical = transform.up * _yMovement;

        // Calculate movement vector
        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * movementSpeed;

        //Apply movement
        motor.Move(_velocity);
        if (Input.GetButton("Master Rotation"))
        {
            //Calculate rotation: this is only used for turning
            float _yRotation = Input.GetAxisRaw("Mouse X");
            float _xRotation = Input.GetAxisRaw("Mouse Y");
            Vector3 _rotation = new Vector3(_xRotation, _yRotation, 0f) * lookSensitivity;

            motor.RotateCamera(_rotation);
        } else
        {
            motor.RotateCamera(new Vector3(0f, 0f, 0f));
        }

        if (Input.GetButton("Reset Rotation"))
        {
            motor.ResetRotation();

        }

        RaycastHit _hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out _hit,maxSpawnDistance, groundLayer))
        {
            cursor.transform.position = _hit.point;
        }
    }
}
