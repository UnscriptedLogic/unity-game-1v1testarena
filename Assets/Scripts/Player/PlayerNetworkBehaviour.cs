using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace Player
{
    public class PlayerNetworkBehaviour : NetworkBehaviour
    {
        private InputManager inputManager;

        [Header("Camera Controls")]
        [SerializeField] private float cameraSens = 50f;
        [SerializeField] private Camera cam;

        private float xRotation, yRotation;

        [Header("Movement Controls")]
        [SerializeField] private float speed = 10f;
        [SerializeField] private float jumpForce = 10f;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private Vector3 groundedCheckPos;
        [SerializeField] private float groundedCheckRadius;
        [SerializeField] private LayerMask jumpableLayer;

        private Vector2 inputDir;
        private bool isGrounded;

        private void Start()
        {
            //Code here executed regardless of ownership

            if (!IsOwner) return;

            //Code here executed only when owned

            //Input Events
            inputManager = InputManager.instance;
            inputManager.onMouseMovement += CameraMovement;
            inputManager.OnCharacterMovementStarted += OnCharacterMovementStarted;
            inputManager.OnCharacterMovementEnded += OnCharacterMovementCanceled;
            inputManager.OnJumpPerformed += OnJumpPerformed;

            //Making sure the main camera is disabled so no 2 cameras are active at once
            Camera.main.gameObject.SetActive(false);
            cam.gameObject.SetActive(true);

            //This is temporary. Will shift this somewhere else
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void OnJumpPerformed()
        {
            if (isGrounded)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }

        private void Update()
        {
            if (!IsOwner) return;

            isGrounded = Physics.CheckSphere(transform.position + groundedCheckPos, groundedCheckRadius, jumpableLayer);
        }

        private void FixedUpdate()
        {
            if (inputDir.magnitude > 0f)
            {
                rb.MovePosition(transform.position + ((transform.forward * inputDir.y + transform.right * inputDir.x) * speed * Time.fixedDeltaTime));
            }
        }

        private void OnCharacterMovementStarted(Vector2 dir) => inputDir = dir;
        private void OnCharacterMovementCanceled(Vector2 dir) => inputDir = dir;

        private void CameraMovement(float mouseX, float mouseY)
        {
            mouseX *= Time.deltaTime * cameraSens;
            mouseY *= Time.deltaTime * cameraSens;

            yRotation += mouseX;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            cam.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
            transform.rotation = Quaternion.Euler(0f, yRotation, 0f);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position + groundedCheckPos, groundedCheckRadius);
        }
    }
}