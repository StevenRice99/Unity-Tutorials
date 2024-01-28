using UnityEngine;

/// <summary>
/// Add health to an object.
/// Slides used in:
/// 6 - Rigidbodies, Colliders, and Triggers
/// </summary>
public class Health : MonoBehaviour
{
    /// <summary>
    /// The current health.
    /// </summary>
    [Tooltip("The current health.")]
    [Min(0)]
    public int value = 3;
}