using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {
    [SerializeField]
    private float movementSpeed = 5f;
    [SerializeField]
    private float lookSensitivity = 1.0f;

    private PlayerMotor motor;

    private void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }

    private void Update()
    {
        float _xMovement = Input.GetAxisRaw("Horizontal");
        float _zMovement = Input.GetAxisRaw("Vertical");

        Vector3 _moveHorizontal = transform.right * _xMovement;
        Vector3 _moveVertical = transform.forward * _zMovement;

        // Calculate movement vector
        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * movementSpeed;

        //Apply movement
        motor.Move(_velocity);

        //Calculate rotation: this is only used for turning
        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _rotation = new Vector3(0f, _yRotation, 0f) * lookSensitivity;

        motor.Rotate(_rotation);

    }
}
