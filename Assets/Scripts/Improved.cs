using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// An improved version of our "Test" script where we learn more advanced concepts.
/// Slides used in:
/// 10 - Advanced Scripting
/// </summary>
[RequireComponent(typeof(MeshRenderer))]
public class Improved : MonoBehaviour
{
    /// <summary>
    /// The data for how this cube behaves.
    /// </summary>
    [Tooltip("The data for how this cube behaves.")]
    [SerializeField]
    private CubeData data;
    
    /// <summary>
    /// The transform of this object.
    /// </summary>
    private Transform _t;

    /// <summary>
    /// The material to change the color of.
    /// </summary>
    private Material _material;

    /// <summary>
    /// If the object is currently set to the starting color.
    /// </summary>
    private bool _isStartingColor = true;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        // Cache the transform.
        _t = transform;
        
        // Cache the position as it is utilized twice in the next line.
        Vector3 position = _t.position;
        
        // Set to the starting position.
        _t.position = new(position.x, position.y, data.positions.x);

        // Get the material we will change the color of.
        _material = GetComponent<MeshRenderer>().material;
    }

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    private void Start()
    {
        // Set the starting material.
        _material.color = Color.red;
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    private void Update()
    {
        // Start changing color when the C key is pressed.
        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            // Stop any previous requests to change the color.
            StopAllCoroutines();
            
            // Call the coroutine to change the color.
            StartCoroutine(ChangeColor());
        }
        
        // Cache the position for performance since otherwise we would be accessing it multiple times.
        Vector3 position = _t.position;
        
        // Ensure the object can not move past its limit.
        if (position.z >= data.positions.y)
        {
            // Set the position we want to start at on the Z (blue) axis, keeping the X (red) and Y (green) axis the same.
            _t.position = new(position.x, position.y, data.positions.y);
            return;
        }

        // Move the object when the space key is pressed.
        if (Keyboard.current.spaceKey.isPressed)
        {
            // Update the Z (blue) position by the speed scaled to the amount of time this frame took.
            _t.position += new Vector3(0, 0, data.speed * Time.deltaTime);
        }
    }

    /// <summary>
    /// Change color gradually over a period of time, fading between the two colors.
    /// </summary>
    private IEnumerator ChangeColor()
    {
        // The color to start the fading from.
        Color start;
        
        // The variable to end the fading at.
        Color end;
        
        // Check if the object is currently set to the starting color.
        if (_isStartingColor)
        {
            // Start with the starting color as this is what it is currently set to.
            start = data.startingColor;
            
            // End at the other color.
            end = data.otherColor;
        }
        else
        {
            // Start at the other color as this is what it is currently set to.
            start = data.otherColor;
            
            // Return to its initial starting color.
            end = data.startingColor;
        }

        // Store how much the change in colors has progress in a range of [0, 1].
        float progress = 0;
        
        // Loop until complete.
        while (progress < 1)
        {
            // Set the color to the value between the start and end colors.
            _material.color = Color.Lerp(start, end, progress);
            
            // Update how much time has passed.
            progress += Time.deltaTime / data.changeDelay;
            
            // Pause the coroutine until the next frame.
            yield return null;
        }
        
        // Update what color the material is currently set to.
        _isStartingColor = !_isStartingColor;
    }
}