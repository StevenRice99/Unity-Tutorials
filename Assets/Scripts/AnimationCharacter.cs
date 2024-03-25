using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Simple character controller with animations.
/// Slides used in:
/// 19 - Animations
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class AnimationCharacter : MonoBehaviour
{
    /// <summary>
    /// How fast to move.
    /// </summary>
    [Tooltip("How fast to move.")]
    [Min(float.Epsilon)]
    [SerializeField]
    private float moveSpeed = 10;
    
    /// <summary>
    /// How much movement can change.
    /// </summary>
    [Tooltip("How much movement can change.")]
    [Min(float.Epsilon)]
    [SerializeField]
    private float speedDelta = 5;
    
    /// <summary>
    /// How fast to turn in degrees.
    /// </summary>
    [Tooltip("How fast to turn in degrees.")]
    [Min(float.Epsilon)]
    [SerializeField]
    private float lookSpeed = 180;
    
    /// <summary>
    /// The character controller to move.
    /// </summary>
    private CharacterController _controller;
    
    /// <summary>
    /// The animator to control.
    /// </summary>
    private Animator _animator;

    /// <summary>
    /// The velocity for movement.
    /// </summary>
    private float _velocity;
    
    /// <summary>
    /// Cached reference for moving for better performance.
    /// </summary>
    private static readonly int Move = Animator.StringToHash("Move");
    
    /// <summary>
    /// Cached referencing for attacking for better performance. 
    /// </summary>
    private static readonly int Attack = Animator.StringToHash("Attack");

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    private void Start()
    {
        // Get the character controller that we will be using.
        _controller = GetComponent<CharacterController>();

        // Get the animator to control.
        _animator = GetComponentInChildren<Animator>();
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

        // Scale to get the desired max movement.
        move *= moveSpeed;
        
        // Only allow the speed to change by the set amount at a time.
        _velocity = Mathf.Lerp(_velocity, move, speedDelta * Time.deltaTime);

        // Calculate the forwards and backwards movement relative to the direction the character is facing.
        Vector3 movement = t.forward * (_velocity * Time.deltaTime);

        // Apply the walking movement and the vertical velocity to the character.
        _controller.Move(new(movement.x, 0, movement.z));
        
        // Pass the movement to the animator.
        _animator.SetFloat(Move, _velocity / moveSpeed);

        // Determine if we should attack.
        _animator.SetBool(Attack, Keyboard.current.spaceKey.wasPressedThisFrame);
    }
}