using UnityEngine;

/// <summary>
/// A sample projectile firing in the direction it starts in.
/// Slides used in:
/// 9 - Interacting with the World
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    /// <summary>
    /// How fast to move the projectile.
    /// </summary>
    [Tooltip("How fast to move the projectile.")]
    [Min(float.Epsilon)]
    [SerializeField]
    private float speed = 100;

    /// <summary>
    /// How long the projectile can last before being destroyed.
    /// </summary>
    [Tooltip("How long the projectile can last before being destroyed.")]
    [Min(float.Epsilon)]
    [SerializeField]
    private float time = 10;
    
    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    private void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddRelativeForce(0, 0, speed, ForceMode.VelocityChange);
        Destroy(gameObject, time);
    }

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider.
    /// In contrast to OnTriggerEnter, OnCollisionEnter is passed the Collision class and not a Collider.
    /// The Collision class contains information, for example, about contact points and impact velocity.
    /// Notes:
    /// Collision events are only sent if one of the colliders also has a non-kinematic rigidbody attached.
    /// Collision events will be sent to disabled MonoBehaviours, to allow enabling Behaviours in response to collisions.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision event.</param>
    private void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject);
    }
}