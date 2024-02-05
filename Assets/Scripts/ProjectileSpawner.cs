using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Sample to spawn projectiles when left clicking with the mouse.
/// Slides used in:
/// 9 - Interacting with the World
/// </summary>
public class ProjectileSpawner : MonoBehaviour
{
    /// <summary>
    /// The prefab to spawn.
    /// </summary>
    [Tooltip("The prefab to spawn.")]
    [SerializeField]
    private GameObject projectilePrefab;

    /// <summary>
    /// Where to spawn the prefabs at.
    /// </summary>
    [Tooltip("Where to spawn the prefabs at.")]
    [SerializeField]
    private Transform spawnPoint;

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    private void Update()
    {
        // Spawn the projectile when the left mouse button is pressed.
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            // Spawn at the spawn point's position and rotation.
            Instantiate(projectilePrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
