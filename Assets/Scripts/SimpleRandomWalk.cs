/// <summary>
/// A simple random walk which simply chooses to place a floor tile in a random direction.
/// Slides used in:
/// 22 - Procedural Generation
/// </summary>
public class SimpleRandomWalk : RandomWalkBase
{
    /// <summary>
    /// Random walk logic.
    /// </summary>
    protected override void Logic()
    {
        // Start at the middle.
        int x = Mid;
        int y = Mid;

        // Loop for the given number of steps.
        for (int step = 0; step < steps; step++)
        {
            // Choose which direction to move in.
            switch (Generator.NextInt(0, 4))
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