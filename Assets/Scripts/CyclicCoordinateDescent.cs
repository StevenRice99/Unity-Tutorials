using UnityEngine;

/// <summary>
/// Basic Cyclic Coordinate Descent (CCD) inverse kinematics.
/// Slides used in:
/// 20 - Inverse Kinematics
/// </summary>
public class CyclicCoordinateDescent : MonoBehaviour
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
    /// How many iterations to run CCD each frame.
    /// </summary>
    [Tooltip("How many iterations to run CCD each frame.")]
    [Min(1)]
    [SerializeField]
    private int iterations = 10;
    
    /// <summary>
    /// The acceptable distance to stop running CCD.
    /// </summary>
    [Tooltip("The acceptable distance to stop running CCD.")]
    [Min(float.Epsilon)]
    [SerializeField]
    private float error = 0.001f;
    
    /// <summary>
    /// If the chain has gotten close enough to the target.
    /// </summary>
    private bool Reached => Vector3.Distance(joints[^1].position, target.position) <= error;

    /// <summary>
    /// LateUpdate is called every frame, if the Behaviour is enabled.
    /// </summary>
    private void LateUpdate()
    {
        // If the target has been reached, there is no reason to run inverse kinematics.
        if (Reached)
        {
            return;
        }
        
        // Loop for the set number of iterations.
        for (int iteration = 0; iteration < iterations; iteration++)
        {
            // Loop through all joints starting from the end, except the last one which simply serves as the end effector.
            for (int joint = joints.Length - 2; joint >= 0; joint--)
            {
                // The difference from the current joint to the end effector.
                Vector3 differenceEffector = joints[^1].position - joints[joint].position;

                // The difference from the current joint to the target.
                Vector3 differenceTarget = target.position - joints[joint].position;
                
                // Apply the rotation.
                joints[joint].rotation = Quaternion.FromToRotation(differenceEffector, differenceTarget) * joints[joint].rotation;

                // If the target has been reached, there is no reason to run more calculations.
                if (Reached)
                {
                    return;
                }
            }
        }
    }
}