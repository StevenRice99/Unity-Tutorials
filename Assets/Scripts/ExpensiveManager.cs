using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A sample implementation of a singleton.
/// Slides used in:
/// 11 - Singleton Pattern
/// </summary>
public class ExpensiveManager : MonoBehaviour
{
    /// <summary>
    /// The singleton instance of the manager.
    /// </summary>
    private static ExpensiveManager _singleton;

    /// <summary>
    /// All items the singleton manages.
    /// </summary>
    private readonly List<Expensive> _items = new();

    /// <summary>
    /// The index of the list to perform on during a given frame.
    /// </summary>
    private int _index;

    /// <summary>
    /// Add an item to the list.
    /// </summary>
    /// <param name="item">The item to add to the list.</param>
    public static void Add(Expensive item)
    {
        // Check if the item is already in the list.
        if (!_singleton._items.Contains(item))
        {
            // Add the item to the list.
            _singleton._items.Add(item);
        }
    }

    /// <summary>
    /// Remove an item from the list.
    /// </summary>
    /// <param name="item">The item to remove from the list.</param>
    public static void Remove(Expensive item)
    {
        // Remove the item from the list.
        _singleton._items.Remove(item);
    }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        // If there is no singleton, set it.
        if (_singleton == null)
        {
            // Set the singleton.
            _singleton = this;
            
            // Have this singleton persist between scenes.
            DontDestroyOnLoad(gameObject);
            return;
        }

        // If there is already a singleton, and we double check this is not the singleton.
        if (_singleton != this)
        {
            // Destroy the other instance so the singleton keeps its single instance.
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    private void Update()
    {
        // If there are no items, there is nothing to perform on.
        if (_items.Count == 0)
        {
            return;
        }

        // Increment the item to perform on.
        _index++;

        // Check if all items have been performed on.
        if (_index >= _items.Count)
        {
            // Loop back to the start.
            _index = 0;
        }
        
        // Perform on only one item.
        _items[_index].Logic();
    }
}