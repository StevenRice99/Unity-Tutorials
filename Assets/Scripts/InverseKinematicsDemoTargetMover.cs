using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// A target to control inverse kinematics in the demo.
/// Slides used in:
/// 20 - Inverse Kinematics
/// </summary>
public class InverseKinematicsDemoTargetMover : MonoBehaviour
{
    /// <summary>
    /// How fast to move the target.
    /// </summary>
    [Tooltip("How fast to move the target.")]
    [Min(float.Epsilon)]
    [SerializeField]
    private float speed = 10;

    /// <summary>
    /// If automatic CCD should be run or if you wish to manually step through it.
    /// </summary>
    [Tooltip("If automatic CCD should be run or if you wish to manually step through it.")]
    [SerializeField]
    private bool isAuto = true;

    /// <summary>
    /// The automatic CCD solver.
    /// </summary>
    [Tooltip("The automatic CCD solver.")]
    [SerializeField]
    private CyclicCoordinateDescent auto;

    /// <summary>
    /// The manual CCD solver.
    /// </summary>
    [Tooltip("The manual CCD solver.")]
    [SerializeField]
    private CyclicCoordinateDescentDemo manual;

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    private void Start()
    {
        // Enable the selected CCD mode.
        SetAuto();
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    private void Update()
    {
        // Change what mode is selected when C is pressed.
        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            isAuto = !isAuto;
            SetAuto();
        }
        
        // Start with no movement.
        Vector3 movement = Vector3.zero;

        // Scale the movement with time.
        float deltaSpeed = speed * Time.deltaTime;
        
        // Move up if the W key is pressed.
        if (Keyboard.current.wKey.isPressed)
        {
            movement.y += deltaSpeed;
        }
        
        // Move down if the S key is pressed.
        if (Keyboard.current.sKey.isPressed)
        {
            movement.y -= deltaSpeed;
        }
        
        // Move right if the D key is pressed.
        if (Keyboard.current.dKey.isPressed)
        {
            movement.x += deltaSpeed;
        }
        
        // Move left if the A key is pressed.
        if (Keyboard.current.aKey.isPressed)
        {
            movement.x -= deltaSpeed;
        }
        
        // Move away if the E key is pressed.
        if (Keyboard.current.eKey.isPressed)
        {
            movement.z += deltaSpeed;
        }
        
        // Move closer if the Q key is pressed.
        if (Keyboard.current.qKey.isPressed)
        {
            movement.z -= deltaSpeed;
        }

        // Apply the movement.
        transform.position += movement;
    }

    /// <summary>
    /// Set which CCD solver to use.
    /// </summary>
    private void SetAuto()
    {
        auto.enabled = isAuto;
        manual.enabled = !isAuto;
    }
}