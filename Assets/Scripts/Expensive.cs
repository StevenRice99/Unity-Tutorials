using UnityEngine;

/// <summary>
/// A sample class that serves the purpose of being costly to run.
/// Slides used in:
/// 11 - Singleton Pattern
/// </summary>
public class Expensive : MonoBehaviour
{
    /// <summary>
    /// How many times to log.
    /// </summary>
    [Tooltip("How many times to log.")]
    [Min(1)]
    [SerializeField]
    private int count = 10;
    
    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    private void OnEnable()
    {
        // Add this to the singleton manager.
        ExpensiveManager.Add(this);
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled.
    /// </summary>
    private void OnDisable()
    {
        // Remove this from the singleton manager.
        ExpensiveManager.Remove(this);
    }

    /// <summary>
    /// The sample expensive logic to run.
    /// </summary>
    public void Logic()
    {
        // Print a message to the console the given number of times.
        for (int i = 0; i < count; i++)
        {
            // Print the message to the console.
            Debug.Log($"{name} - This is an expensive method {i + 1} / {count}");
        }
    }
}