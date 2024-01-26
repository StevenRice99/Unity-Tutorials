using UnityEngine;

/// <summary>
/// Explore how physics, rigidbodies, colliders, and triggers work.
/// Slides used in:
/// 6 - Rigidbodies, Colliders, and Triggers
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PhysicsTester : MonoBehaviour
{
    /// <summary>
    /// How much for to add to the object each physics step.
    /// </summary>
    [Tooltip("How much for to add to the object.")]
    [SerializeField]
    private float force = 1;
    
    /// <summary>
    /// Get the rigidbody to handle physics on this object.
    /// </summary>
    private Rigidbody _rb;

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Frame-rate independent MonoBehaviour.FixedUpdate message for physics calculations.
    /// </summary>
    private void FixedUpdate()
    {
        // Add the force to the rigidbody every physics tick.
        _rb.AddForce(0, 0, force);
        
        // Move when the rigidbody is set to kinematic.
        //_rb.MovePosition(transform.position + new Vector3(0, 0, force * Time.fixedDeltaTime));
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
        // Check if the object the rigidbody collided with has a health component attached to it.
        Health health = other.gameObject.GetComponent<Health>();
        if (health != null)
        {
            // If it does have health, reduce the health.
            health.value--;
            
            // Check if the object is out of health.
            if (health.value <= 0)
            {
                // Destroy the object that the rigidbody collided with if it out of health.
                Destroy(other.gameObject);
            }
        }
        
        // Destroy this object that the rigidbody is attached to.
        Destroy(gameObject);
    }

    /// <summary>
    /// When a GameObject collides with another GameObject, Unity calls OnTriggerEnter.
    /// OnTriggerEnter happens on the FixedUpdate function when two GameObjects collide.
    /// The Colliders involved are not always at the point of initial contact.
    /// Note:
    /// Both GameObjects must contain a Collider component.
    /// One must have Collider.isTrigger enabled, and contain a Rigidbody.
    /// If both GameObjects have Collider.isTrigger enabled, no collision happens.
    /// The same applies when both GameObjects do not have a Rigidbody component.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Entered Trigger {other}");
    }
}