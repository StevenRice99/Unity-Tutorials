using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Sample to ray cast/hit scan when right clicking with the mouse.
/// Slides used in:
/// 9 - Interacting with the World
/// </summary>
[RequireComponent(typeof(LineRenderer))]
public class HitScan : MonoBehaviour
{
    /// <summary>
    /// Where to start the ray from.
    /// </summary>
    [Tooltip("Where to start the ray from.")]
    [SerializeField]
    private Transform rayStart;

    /// <summary>
    /// The line renderer for our hit scan visualization.
    /// </summary>
    private LineRenderer _lr;

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    private void Start()
    {
        _lr = GetComponent<LineRenderer>();
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    private void Update()
    {
        // Do nothing when the right mouse button is not pressed.
        if (!Mouse.current.rightButton.wasPressedThisFrame)
        {
            return;
        }

        // Cache the start position.
        Vector3 p = rayStart.position;
        
        // Perform the ray cast/hit scan, returning true if something is hit.
        if (Physics.Raycast(p, rayStart.forward, out RaycastHit hit))
        {
            // If something is hit, set the positions to it.
            _lr.SetPositions(new []{p, hit.point});
        }
        else
        {
            // Otherwise, find a point in the distance to draw the ray.
            _lr.SetPositions(new []{p, p + rayStart.TransformDirection(Vector3.forward) * 1000});
        }
    }
}