using UnityEngine;

/// <summary>
/// A random walk which turns by chance.
/// Slides used in:
/// 22 - Procedural Generation
/// </summary>
public class TurnRandomWalk : RandomWalkBase
{
    /// <summary>
    /// The chance to turn the direction the random walk is in.
    /// </summary>
    [Tooltip("The chance to turn the direction the random walk is in.")]
    [Range(0, 1)]
    [SerializeField]
    protected float turnChance = 0.2f;
    
    /// <summary>
    /// Random walk logic.
    /// </summary>
    protected override void Logic()
    {
        // Start at the middle.
        int x = Mid;
        int y = Mid;

        // Choose the direction to start in.
        int direction = Generator.NextInt(0, 4);

        // Loop for the given number of steps.
        for (int step = 0; step < steps; step++)
        {
            // Determine if the direction should change.
            if (Generator.NextFloat() <= turnChance)
            {
                direction = Generator.NextInt(0, 4);
            }
            
            // Move in the selected direction.
            switch (direction)
            {
                case 3:
                    x--;
                    break;
                case 2:
                    x++;
                    break;
                case 1:
                    y--;
                    break;
                default:
                    y++;
                    break;
            }
            
            // Set the floor at the new position.
            SetFloor(x, y);
        }
    }
}