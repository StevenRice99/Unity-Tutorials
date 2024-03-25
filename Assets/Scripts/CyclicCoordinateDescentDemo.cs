using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Basic Cyclic Coordinate Descent (CCD) inverse kinematics which you can manually run every frame.
/// Slides used in:
/// 20 - Inverse Kinematics
/// </summary>
public class CyclicCoordinateDescentDemo : MonoBehaviour
{
    /// <summary>
    /// The joints to control.
    /// </summary>
    [Tooltip("The joints to control.")]
    [SerializeField]
    private Transform[] joints;
    
    /// <summary>
    /// The target to reach for.
    /// </summary>
    [Tooltip("The target to reach for.")]
    public Transform target;
    
    /// <summary>
    /// The acceptable distance to stop running CCD.
    /// </summary>
    [Tooltip("The acceptable distance to stop running CCD.")]
    [Min(float.Epsilon)]
    [SerializeField]
    private float error = 0.001f;

    /// <summary>
    /// The currently selected joint.
    /// </summary>
    private int _joint;

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    private void Start()
    {
        // Start at the last joint besides the end effector.
        _joint = joints.Length - 2;
    }

    /// <summary>
    /// LateUpdate is called every frame, if the Behaviour is enabled.
    /// </summary>
    private void LateUpdate()
    {
        // If R is pressed, reset the link number so the next manual steps start from the last joint.
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            _joint = joints.Length - 2;
        }
        
        // Run a step of CCD only when space is pressed.
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Logic();
        }
    }

    private void Logic()
    {
        // If the target has been reached, there is no reason to run inverse kinematics.
        if (Vector3.Distance(joints[^1].position, target.position) <= error)
        {
            // Reset to the last joint besides the end effector for future inverse kinematics.
            _joint = joints.Length - 2;
            return;
        }
        
        // The difference from the current joint to the end effector.
        Vector3 differenceEffector = joints[^1].position - joints[_joint].position;

        // The difference from the current joint to the target.
        Vector3 differenceTarget = target.position - joints[_joint].position;
                
        // Apply the rotation.
        joints[_joint].rotation = Quaternion.FromToRotation(differenceEffector, differenceTarget) * joints[_joint].rotation;

        // Move to the next joint.
        if (--_joint < 0)
        {
            // If all joints have been run, go back to the last joint besides the end effector.
            _joint = joints.Length - 2;
        }
    }
}