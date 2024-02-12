using UnityEngine;

/// <summary>
/// Hold the data for our "Improved" cubes.
/// Slides used in:
/// 10 - Advanced Scripting
/// </summary>
[CreateAssetMenu(fileName = "Cube Data", menuName = "Scriptable Objects/Cube Data", order = 0)]
public class CubeData : ScriptableObject
{
    /// <summary>
    /// How fast to move the object.
    /// </summary>
    [Tooltip("How fast to move the object.")]
    [Min(float.Epsilon)]
    public float speed = 1;

    /// <summary>
    /// Where to start and end moving the object from.
    /// </summary>
    [Tooltip("Where to start and end moving the object from.")]
    public Vector2 positions = new (-5, 5);

    /// <summary>
    /// The color to start as.
    /// </summary>
    [Tooltip("The color to start as.")]
    public Color startingColor = Color.red;

    /// <summary>
    /// The color to switch between.
    /// </summary>
    [Tooltip("The color to switch between.")]
    public Color otherColor = Color.blue;

    /// <summary>
    /// The amount of time to wait before changing colors.
    /// </summary>
    [Tooltip("The amount of time to wait before changing colors.")]
    [Min(float.Epsilon)]
    public float changeDelay = 1;

    /// <summary>
    /// Editor-only function that Unity calls when the script is loaded or a value changes in the Inspector.
    /// </summary>
    private void OnValidate()
    {
        // If the minimum position is greater than the maximum position.
        if (positions.x > positions.y)
        {
            // Swap them so they are valid.
            (positions.x, positions.y) = (positions.y, positions.x);
        }
    }
}
