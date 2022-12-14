using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    //Singleton
    public static InputManager instance;

    private InputControls inputControls;
    private InputControls.DefaultControlsActions actionMap;

    //Delegates and events for other scripts to listen to
    public delegate void MouseMovement(float x, float y);
    public delegate void CharacterMovementStarted(Vector2 dir);
    public delegate void CharacterMovementEnded(Vector2 dir);
    public delegate void JumpPerformed();
    public delegate void UseItem();

    public event MouseMovement onMouseMovement;
    public event CharacterMovementStarted OnCharacterMovementStarted;
    public event CharacterMovementEnded OnCharacterMovementEnded;
    public event JumpPerformed OnJumpPerformed;
    public event UseItem OnUseItemPerformed;
    public event UseItem OnUseItemCancelled;

    private float mouseX;
    private float mouseY;

    private void Awake()
    {
        instance = this;
        inputControls = new InputControls();
    }

    private void OnEnable()
    {
        inputControls.Enable();
        actionMap = inputControls.DefaultControls;

        //Camera
        actionMap.CameraAxisX.performed += CameraAxisXPerformed;
        actionMap.CameraAxisY.performed += CameraAxisYPerformed;
        actionMap.CameraMovementDetector.performed += OnCameraMovement;

        //Movement
        actionMap.Movement.performed += OnMovementPerformed;
        actionMap.Movement.canceled += MovementCanceled;

        //Jump
        actionMap.Jump.performed += OnCharacterJumpPerformed;

        actionMap.UseItem.performed += UseItemPerformed;
        actionMap.UseItem.canceled += UseItemCancelled;
    }


    private void OnDisable()
    {
        inputControls.Disable();
    }

    //Camera
    private void OnCameraMovement(InputAction.CallbackContext context) => onMouseMovement?.Invoke(mouseX, mouseY);
    private void CameraAxisXPerformed(InputAction.CallbackContext context) => mouseX = context.ReadValue<float>();
    private void CameraAxisYPerformed(InputAction.CallbackContext context) => mouseY = context.ReadValue<float>();

    //Movement
    private void OnMovementPerformed(InputAction.CallbackContext context) => OnCharacterMovementStarted?.Invoke(context.ReadValue<Vector2>());
    private void MovementCanceled(InputAction.CallbackContext context) => OnCharacterMovementEnded?.Invoke(context.ReadValue<Vector2>());

    //Jump
    private void OnCharacterJumpPerformed(InputAction.CallbackContext obj) => OnJumpPerformed?.Invoke();

    private void UseItemPerformed(InputAction.CallbackContext context) => OnUseItemPerformed?.Invoke();
    private void UseItemCancelled(InputAction.CallbackContext context) => OnUseItemCancelled?.Invoke();
}
