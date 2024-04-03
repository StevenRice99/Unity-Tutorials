using UnityEngine;

/// <summary>
/// A simple example of calling a generic we made.
/// Slides used in:
/// 23 - Mastering C#
/// </summary>
public class GenericExample : MonoBehaviour
{
    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    private void Start()
    {
        // Call our generic method.
        Collider c = Generics.GetComponentOnCurrentOrChildren<Collider>(this);

        // If a collider was found, make it a trigger.
        if (c != null)
        {
            c.isTrigger = true;
        }

        // Call our generic method to find the nearest health object.
        Health h = Generics.Closest<Health>(transform.position);
        
        // If there was one, log its name.
        if (h != null)
        {
            Debug.Log(h.name);
        }
    }
}