using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Rigidbody rb;
    private Camera playerCamera;
    private float verticalLookRotation;
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Quaternion cameraInitialRotation;

    private GameObject ballToPickup;
    private GameObject pickedUpBall;
    private Rigidbody pickedUpBallRb;
    private bool isBallPickedUp = false;
   
    private void Start() {
        rb = GetComponent<Rigidbody>();
        playerCamera = Camera.main;
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        cameraInitialRotation = playerCamera.transform.rotation;
        rb.mass = GameConfig.PlayerWeight;
    }

    public void Initialize() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    public void DeInitialize() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        ResetPosition();
        ClearReferences();
    }

    private void ResetPosition() {
        rb.interpolation = RigidbodyInterpolation.None;
        rb.constraints = RigidbodyConstraints.None;

        transform.position = initialPosition;
        transform.rotation = initialRotation;
        playerCamera.transform.rotation = cameraInitialRotation;

        rb.velocity = Vector3.zero;
    }

    public void HandleMovement() {
        float mouseX = Input.GetAxis("Mouse X") * InputConfig.MouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * InputConfig.MouseSensitivity;

        verticalLookRotation -= mouseY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);

        playerCamera.transform.localRotation = Quaternion.Euler(verticalLookRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.right * moveX + transform.forward * moveZ;
        Vector3 velocity = moveDirection * InputConfig.MoveSpeed;
        velocity.y = rb.velocity.y; 

        rb.velocity = velocity;
    }

    public void HandlePickupAndThrow() {
        if (isBallPickedUp) {
            if (Input.GetMouseButtonDown(0)) { 
                ThrowObject(Vector3.forward);
            } else if (Input.GetMouseButtonDown(1)) { 
                ThrowObject(Vector3.forward + Vector3.up);
            }

            GameManager.Instance.GameplayHUDController.SetTarget(false);
        } else {
            Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, InputConfig.PickupRange)) {
                if (hit.collider.CompareTag("Pickupable")) {
                    ballToPickup = hit.collider.gameObject;
                    GameManager.Instance.GameplayHUDController.SetTarget(pickedUpBall == null);
                    if (Input.GetKeyDown(KeyCode.E)) {
                        PickupBall(ballToPickup);
                    }
                } else {
                    GameManager.Instance.GameplayHUDController.SetTarget(false);
                }
            } else {
                ballToPickup = null;
                GameManager.Instance.GameplayHUDController.SetTarget(false);
            }
        }
    }

    private void PickupBall(GameObject ball) {
        pickedUpBall = ball;
        pickedUpBallRb = ball.GetComponent<Rigidbody>();

        SphereCollider sphereCollider = pickedUpBall.GetComponent<SphereCollider>();
        sphereCollider.enabled = false;

        if (pickedUpBallRb != null) {
            pickedUpBallRb.isKinematic = true;
        }

        pickedUpBall.transform.SetParent(playerCamera.transform);
        pickedUpBall.transform.localPosition = new Vector3(0, 0, InputConfig.PickupCameraDistance); 
        isBallPickedUp = true;
    }

    private void ThrowObject(Vector3 direction) {
        if (pickedUpBall != null && pickedUpBallRb != null) {
            pickedUpBall.transform.SetParent(GameManager.Instance.PoolManager.container);
            pickedUpBallRb.isKinematic = false; 

            SphereCollider sphereCollider = pickedUpBall.GetComponent<SphereCollider>();
            sphereCollider.enabled = true;

            Vector3 throwDirection = playerCamera.transform.TransformDirection(direction.normalized);
            pickedUpBallRb.AddForce(throwDirection * InputConfig.BallThrowForce, ForceMode.VelocityChange);

            ClearReferences();
        }
    }

    private void ClearReferences() {
        pickedUpBall = null;
        pickedUpBallRb = null;
        isBallPickedUp = false;
    }
}
