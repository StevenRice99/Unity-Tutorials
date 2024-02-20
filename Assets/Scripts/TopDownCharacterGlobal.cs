using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// A simple controller to just move with WASD in global space.
/// Slides used in:
/// 13 - Artificial Intelligence
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class TopDownCharacterGlobal : MonoBehaviour
{
    /// <summary>
    /// How fast to move the character.
    /// </summary>
    [Tooltip("How fast to move the character.")]
    [SerializeField]
    private float moveSpeed = 10;
    
    /// <summary>
    /// The controller to move.
    /// </summary>
    [HideInInspector]
    [SerializeField]
    private CharacterController controller;

    /// <summary>
    /// Editor-only function that Unity calls when the script is loaded or a value changes in the Inspector.
    /// </summary>
    private void OnValidate()
    {
        // Check if there is no controller.
        if (controller == null)
        {
            // Assign the controller if it is not yet.
            controller = GetComponent<CharacterController>();
        }
    }
    
    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    private void Update()
    {
        // Get the speed relative to the time of this frame.
        float speed = moveSpeed * Time.deltaTime;
        
        // Assign the movement to be zero to start.
        Vector3 movement = Vector3.zero;

        // Check if the W key is pressed.
        if (Keyboard.current.wKey.isPressed)
        {
            // Move positive along the Z axis.
            movement.z += speed;
        }

        // Check if the S key is pressed.
        if (Keyboard.current.sKey.isPressed)
        {
            // Move negative along the Z axis.
            movement.z -= speed;
        }

        // Check if the D key is pressed.
        if (Keyboard.current.dKey.isPressed)
        {
            // Move positive along the X axis.
            movement.x += speed;
        }

        // Check if the A key is pressed.
        if (Keyboard.current.aKey.isPressed)
        {
            // Move negative along the X axis.
            movement.x -= speed;
        }

        // Apply the movement to the controller.
        controller.Move(movement);
    }
}