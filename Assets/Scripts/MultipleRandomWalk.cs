using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A random walk which turns by chance and can by chance create or remove the number of walkers.
/// Slides used in:
/// 22 - Procedural Generation
/// </summary>
public class MultipleRandomWalk : TurnRandomWalk
{
    /// <summary>
    /// How many walkers there can be at most.
    /// </summary>
    [Tooltip("How many walkers there can be at most.")]
    [Min(1)]
    [SerializeField]
    private int maxWalkers = 10;

    /// <summary>
    /// The chance a new walker can spawn.
    /// </summary>
    [Tooltip("The chance a new walker can spawn.")]
    [Range(0, 1)]
    [SerializeField]
    private float walkerSpawnChance = 0.2f;

    /// <summary>
    /// The chance an existing walker can be removed.
    /// </summary>
    [Tooltip("The chance an existing walker can be removed.")]
    [Range(0, 1)]
    [SerializeField]
    private float walkerRemoveChance = 0.2f;
    
    /// <summary>
    /// Random walk logic.
    /// </summary>
    protected override void Logic()
    {
        // Start with one walker at the middle.
        List<int> x = new(maxWalkers) {Mid};
        List<int> y = new(maxWalkers) {Mid};
        
        // Choose a direction for the first walker.
        List<int> direction = new(maxWalkers) {Generator.NextInt(0, 4)};

        // Loop until the given number of steps has been reached.
        int step = 0;
        while (true)
        {
            // Loop through all walkers.
            for (int walker = 0; walker < direction.Count; walker++)
            {
                // Determine if the direction for this walker should change.
                if (Generator.NextFloat() <= turnChance)
                {
                    direction[walker] = Generator.NextInt(0, 4);
                }

                // Move in the selected direction for this walker.
                switch (direction[walker])
                {
                    case 3:
                        x[walker]--;
                        break;
                    case 2:
                        x[walker]++;
                        break;
                    case 1:
                        y[walker]--;
                        break;
                    default:
                        y[walker]++;
                        break;
                }
                
                // Set the floor at the new position of this walker.
                SetFloor(x[walker], y[walker]);
                
                // Increment the steps taken.
                step++;

                // Return if all steps have been taken.
                if (step >= steps)
                {
                    return;
                }

                // If not yet at the max walkers, try to add a new walker starting from where the current walker is.
                if (direction.Count < maxWalkers && Generator.NextFloat() <= walkerSpawnChance)
                {
                    x.Add(x[walker]);
                    y.Add(y[walker]);
                    
                    // Start this walker in a random direction.
                    direction.Add(Generator.NextInt(0, 4));
                }

                // If there is more than one walker, try to remove the current walker.
                if (direction.Count > 1 && Generator.NextFloat() <= walkerRemoveChance)
                {
                    x.RemoveAt(walker);
                    y.RemoveAt(walker);
                    direction.RemoveAt(walker);
                    
                    // Decrement the walker number as this one has been removed.
                    walker--;
                }
            }
        }
    }
}