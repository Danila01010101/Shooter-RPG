using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class SimpleFPSController : MonoBehaviour {

    [SerializeField] private float _speed = 10;
    [SerializeField] private float _gravity = -20f;
    [SerializeField] private float _jumpHeight = 2;
    [SerializeField] private float _runMultiplier = 2;
    [SerializeField, Range(0f, 90f)] private float _jumpSlopeLimit;

    private CharacterController _charController;
    private float _jumpMult;
    private float _yVelocity;
    private float _originalSlopeLimit;

    [Header("Mouse look")]
    private float xRotation = 0f;
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private float _minLookAngle = -85f;
    [SerializeField] private float _maxLookAngle = 75f;
    [SerializeField] private Transform mainCam;

    private void Start() 
    {
        _charController = GetComponent<CharacterController>();

        _originalSlopeLimit = _charController.slopeLimit;
        _jumpMult = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
        xRotation = mainCam.transform.eulerAngles.x;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Equals)) mouseSensitivity = Mathf.Clamp(mouseSensitivity + 20, 20f, 1000f);
        if (Input.GetKeyDown(KeyCode.Minus)) mouseSensitivity = Mathf.Clamp(mouseSensitivity - 20, 20f, 1000f);

        PlayerMove();
    }

    private void LateUpdate() 
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, _minLookAngle, _maxLookAngle);

        mainCam.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }


    private void PlayerMove() 
    {
        if (_charController.isGrounded || _charController.collisionFlags == CollisionFlags.Above) _yVelocity = -0.1f;

        if (_charController.isGrounded) 
        {
            _charController.slopeLimit = _originalSlopeLimit;
        }
        else {
            _charController.slopeLimit = _jumpSlopeLimit;
        }

        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 move = (transform.right * x + transform.forward * z).normalized;
        move = move * _speed * Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftShift)) move *= _runMultiplier;

        if (Input.GetButtonDown("Jump") && _charController.isGrounded) {
            _yVelocity += _jumpMult;
        }

        _yVelocity += _gravity * Time.deltaTime;

        move.y = _yVelocity * Time.deltaTime;

        _charController.Move(move);
    }
}