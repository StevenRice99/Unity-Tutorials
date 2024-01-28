using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// A basic character controller allow you to walk forwards and backwards with W and S, and turn using A and D.
/// Slides used in:
/// 8 - Characters
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class CharacterMover : MonoBehaviour
{
    /// <summary>
    /// How fast to move.
    /// </summary>
    [Tooltip("How fast to move.")]
    [Min(float.Epsilon)]
    [SerializeField]
    private float moveSpeed = 10;
    
    /// <summary>
    /// How fast to turn in degrees.
    /// </summary>
    [Tooltip("How fast to turn in degrees.")]
    [Min(float.Epsilon)]
    [SerializeField]
    private float lookSpeed = 180;

    /// <summary>
    /// How much velocity to add for jumping.
    /// </summary>
    [Tooltip("How much velocity to add for jumping.")]
    [Min(float.Epsilon)]
    [SerializeField]
    private float jumpForce = 0.5f;

    /// <summary>
    /// The gravity to apply to this character.
    /// </summary>
    [Tooltip("The gravity to apply to this character.")]
    [SerializeField]
    private float gravity = -2;

    /// <summary>
    /// The terminal velocity of the character.
    /// </summary>
    [Tooltip("The terminal velocity of the character.")]
    [SerializeField]
    private float terminalVelocity = -1;
    
    /// <summary>
    /// The character controller to move.
    /// </summary>
    private CharacterController _controller;

    /// <summary>
    /// The vertical velocity to handle jumping and falling.
    /// </summary>
    private float _velocity;

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    private void Start()
    {
        // Get the character controller that we will be using.
        _controller = GetComponent<CharacterController>();
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    private void Update()
    {
        // Variables to hold our movement (forwards and back) and looking (right and left) data.
        float move = 0;
        float look = 0;
        
        // Move forward when W is pressed.
        if (Keyboard.current.wKey.isPressed)
        {
            move += 1;
        }

        // Move backwards when S is pressed.
        if (Keyboard.current.sKey.isPressed)
        {
            move -= 1;
        }
        
        // Move right when D is pressed.
        if (Keyboard.current.dKey.isPressed)
        {
            look += 1;
        }

        // Move left when A is pressed.
        if (Keyboard.current.aKey.isPressed)
        {
            look -= 1;
        }

        // Cache the transform for performance since otherwise we would be accessing it multiple times.
        Transform t = transform;
        
        // Rotate on the y (green) axis for turning.
        t.Rotate(0, look * lookSpeed * Time.deltaTime, 0);

        // Calculate the forwards and backwards movement relative to the direction the character is facing.
        Vector3 movement = t.forward * (moveSpeed * move * Time.deltaTime);

        // Handle logic on the ground.
        if (_controller.isGrounded)
        {
            // Jump when the space key is pressed and otherwise set the velocity to 0.
            _velocity = Keyboard.current.spaceKey.wasPressedThisFrame ? jumpForce : 0;
        }
        
        // Every frame apply gravity. This is done even if we are on the ground so the character "pushes" into the
        // ground which will ensure that "isGrounded" has a proper value next frame.
        _velocity += gravity * Time.deltaTime;

        // If you do not want your object to infinitely accelerate, cap it at a terminal velocity.
        if (_velocity < terminalVelocity)
        {
            _velocity = terminalVelocity;
        }

        // Apply the walking movement and the vertical velocity to the character.
        _controller.Move(new(movement.x, _velocity, movement.z));
    }
}