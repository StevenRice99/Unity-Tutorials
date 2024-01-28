using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Utilize the input system to move an object.
/// Slides used in:
/// 4 - Input Handling
/// </summary>
public class TransformMover : MonoBehaviour
{
    /// <summary>
    /// How fast to move.
    /// </summary>
    [Tooltip("How fast to move.")]
    [Min(float.Epsilon)]
    [SerializeField]
    private float moveSpeed = 1;

    /// <summary>
    /// How fast to turn with the mouse.
    /// </summary>
    [Tooltip("How fast to turn with the mouse.")]
    [Min(float.Epsilon)]
    [SerializeField]
    private float lookSpeed = 1;

    /// <summary>
    /// How to move this frame.
    /// In the form (x, y), with x and y in a range of [-1, 1].
    /// Positive Y - Forward.
    /// Negative Y - Backwards.
    /// Positive X - Right.
    /// Negative X - Left.
    /// </summary>
    private Vector2 _move;

    /// <summary>
    /// How to look this frame.
    /// In a range of [-1, 1].
    /// Positive - Right.
    /// Negative - Left.
    /// </summary>
    private float _look;

    /// <summary>
    /// Listen to the message for movement input sent by the "Player Input" component.
    /// </summary>
    /// <param name="value">Wraps around values provided by input actions.</param>
    private void OnMove(InputValue value)
    {
        _move = value.Get<Vector2>();
    }
    
    /// <summary>
    /// Listen to the message for looking input sent by the "Player Input" component.
    /// </summary>
    /// <param name="value">Wraps around values provided by input actions.</param>
    private void OnLook(InputValue value)
    {
        _look = value.Get<float>();
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    private void Update()
    {
        // Cache the transform for performance since otherwise we would be accessing it multiple times.
        Transform t = transform;
        
        // Set the rotation where we only want to look around the Y (green) axis, keeping the others at zero.
        // "Mouse Delta" which we are reading from our input settings is already scaled per-frame
        // which is why we do not need to use "Time.deltaTime" here.
        t.eulerAngles = new(0, t.eulerAngles.y + _look * lookSpeed, 0);

        // Cache the speed value so we don't need to multiple by "Time.deltaTime" twice.
        float speed = moveSpeed * Time.deltaTime;
        
        // Set the position for forward (forwards and backwards) and right (right and left) movement.
        t.position += t.forward * (_move.y * speed) + t.right * (_move.x * speed);
    }
}