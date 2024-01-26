using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Our introduction to scripting.
/// Slides used in:
/// 3 - Scripting
/// 4 - Input Handling
/// </summary>
public class Test : MonoBehaviour
{
    /// <summary>
    /// How fast to move the object.
    /// </summary>
    [Tooltip("How fast to move the object.")]
    [SerializeField]
    private float speed = 1;
    
    /// <summary>
    /// Where to start moving the object from.
    /// </summary>
    [Tooltip("Where to start moving the object from.")]
    [SerializeField]
    private float start = -5;
    
    /// <summary>
    /// Where to end moving the object.
    /// </summary>
    [Tooltip("Where to end moving the object.")]
    [SerializeField]
    private float end = 5;
    
    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    private void Start()
    {
        // Cache the transform for performance since otherwise we would be accessing it multiple times.
        Transform t = transform;
        
        // Cache the position for performance since otherwise we would be accessing it multiple times.
        Vector3 position = t.position;
        
        // Set the position we want to start at on the Z (blue) axis, keeping the X (red) and Y (green) axis the same.
        t.position = new(position.x, position.y, start);
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    private void Update()
    {
        // Cache the transform for performance since otherwise we would be accessing it multiple times.
        Transform t = transform;
        
        // Ensure the object can not move past its limit.
        if (t.position.z >= end)
        {
            // Cache the position for performance since otherwise we would be accessing it multiple times.
            Vector3 position = t.position;
            
            // Set the position we want to start at on the Z (blue) axis, keeping the X (red) and Y (green) axis the same.
            t.position = new(position.x, position.y, end);
            return;
        }

        // Move the object when the space key is pressed.
        if (Keyboard.current.spaceKey.isPressed)
        {
            // Update the Z (blue) position by the speed scaled to the amount of time this frame took.
            t.position += new Vector3(0, 0, speed * Time.deltaTime);
        }
    }
}