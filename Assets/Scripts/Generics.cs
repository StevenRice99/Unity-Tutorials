using System.Linq;
using UnityEngine;

/// <summary>
/// A simple example of a generic method.
/// Slides used in:
/// 23 - Mastering C#
/// </summary>
public static class Generics
{
    /// <summary>
    /// Tries to get the given type of component on the GameObject of the current component.
    /// If one does not exist, it tries finding it in the children.
    /// </summary>
    /// <param name="c">The component/GameObject to find the type on.</param>
    /// <typeparam name="T">The type to look for.</typeparam>
    /// <returns>The first instance of the type, or null if there is none.</returns>
    public static T GetComponentOnCurrentOrChildren<T>(Component c)
    {
        return c.GetComponent<T>() ?? c.GetComponentInChildren<T>();
    }

    /// <summary>
    /// Return the nearest instance of a component.
    /// </summary>
    /// <param name="position">The position to find the nearest component to.</param>
    /// <typeparam name="T">The type of component to find.</typeparam>
    /// <returns>The nearest instance of a component or null if there are no instances.</returns>
    public static T Closest<T>(Vector3 position) where T : Component
    {
        T[] instances = Object.FindObjectsOfType<T>();
        return instances.OrderBy(x => Vector3.Distance(position, x.transform.position)).FirstOrDefault();
    }
}