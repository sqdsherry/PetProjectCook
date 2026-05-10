using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerInteraction playerInteraction;

    [SerializeField] private float speed = 5f;

    private CharacterController characterController;
    private Vector2 movementInput;
    private Vector3 movement;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();

        if (playerInteraction == null)
        {
            playerInteraction = GetComponent<PlayerInteraction>();
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
        movement = new Vector3(movementInput.x, 0, movementInput.y);
    }

    private void FixedUpdate()
    {
        //characterController.Move(movement * speed * Time.deltaTime);
        Vector3 move = movement * speed;
        characterController.Move(move * Time.deltaTime);

        characterController.transform.position =
            new Vector3(characterController.transform.position.x, 1, characterController.transform.position.z);
    }
}
