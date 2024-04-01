using Unity.Mathematics;
using UnityEngine;
using Random = Unity.Mathematics.Random;

/// <summary>
/// Base class for random walk implementation to expand upon.
/// Slides used in:
/// 22 - Procedural Generation
/// </summary>
public abstract class RandomWalkBase : MonoBehaviour
{
    /// <summary>
    /// The seed for the procedural generation.
    /// </summary>
    [Tooltip("The seed for the procedural generation.")]
    [SerializeField]
    private uint seed;
    
    /// <summary>
    /// The number of steps for the random walk.
    /// </summary>
    [Tooltip("The number of steps for the random walk.")]
    [Min(0)]
    [SerializeField]
    protected int steps = 1000;

    /// <summary>
    /// What to place for floors.
    /// </summary>
    [Tooltip("What to place for floors.")]
    [SerializeField]
    protected GameObject floorPrefab;

    /// <summary>
    /// What to place for walls.
    /// </summary>
    [Tooltip("What to place for walls.")]
    [SerializeField]
    protected GameObject wallPrefab;

    /// <summary>
    /// The orientation to place floors in.
    /// </summary>
    [Tooltip("The orientation to place floors in.")]
    [SerializeField]
    protected Vector3 floorOrientation = new(90, 0, 0);

    /// <summary>
    /// The orientation to place walls in.
    /// </summary>
    [Tooltip("The orientation to place walls in.")]
    [SerializeField]
    protected Vector3 wallOrientation = new(0, 0, 0);

    /// <summary>
    /// How high to place walls.
    /// </summary>
    [Tooltip("How high to place walls.")]
    [Min(float.Epsilon)]
    [SerializeField]
    protected float wallOffset = 0.5f;
    
    /// <summary>
    /// The mid point coordinate of the placement grid.
    /// </summary>
    protected int Mid { get; private set; }

    /// <summary>
    /// The random number generator.
    /// </summary>
    protected Random Generator;

    /// <summary>
    /// The data to use for building the level with true values being where floors are.
    /// </summary>
    private bool[][] _data;

    /// <summary>
    /// The size of the floor to place.
    /// </summary>
    private int _size;

    /// <summary>
    /// The minimum space used to help with placement and rendering.
    /// </summary>
    private int2 _min;

    /// <summary>
    /// The maximum space used to help with placement and rendering.
    /// </summary>
    private int2 _max;
    
    /// <summary>
    /// How much to offset spawning by.
    /// </summary>
    private float2 _offset;

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    private void OnEnable()
    {
        // Destroy any previously spawned elements.
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        // If the seed is zero, it means we want a completely random generation. Otherwise, use the seed.
        Generator = new(seed == 0 ? (uint) UnityEngine.Random.Range(1, int.MaxValue) : seed);
        
        // Ensure the data grid we are writing to should not have any out of bounds errors.
        _size = steps * 2 + 1;
        
        // Initialize the data grid to all false.
        _data = new bool[_size][];
        for (int i = 0; i < _data.Length; i++)
        {
            _data[i] = new bool[_size];
        }
        
        // Get the midpoint to help start at.
        Mid = steps;

        // Place the starting tile in the middle.
        _data[Mid][Mid] = true;
        
        // Start the min and max values at the midpoint.
        _min = new(Mid, Mid);
        _max = new(Mid, Mid);

        // Perform the implemented logic.
        Logic();

        // Determine the space which was used.
        int2 used = new(_max.x - _min.x, _max.y - _min.y);
        
        // Determine how much to offset visual placement by.
        _offset = new(used.x / 2f, used.y / 2f);

        // Place all floors.
        for (int x = 0; x < _size; x++)
        {
            for (int y = 0; y < _size; y++)
            {
                if (!_data[x][y])
                {
                    continue;
                }

                Spawn(x, y, 0, floorPrefab, floorOrientation);
                SpawnWall(x - 1, y - 1);
                SpawnWall(x - 1, y);
                SpawnWall(x - 1, y + 1);
                SpawnWall(x + 1, y - 1);
                SpawnWall(x + 1, y);
                SpawnWall(x + 1, y + 1);
                SpawnWall(x, y - 1);
                SpawnWall(x, y + 1);
            }
        }
        
        // Get the camera.
        Camera cam = Camera.main;
        if (cam == null)
        {
            cam = FindObjectOfType<Camera>();
        }

        // If there is a camera, make sure it can see the entire level which has been rendered
        if (cam != null)
        {
            Transform t = cam.transform;
            t.position = new(0, t.position.y, 0);
            bool xLargest = used.x >= used.y;
            t.eulerAngles = new(90, 0, xLargest ? 90 : 0);
            cam.orthographic = true;
            cam.orthographicSize = (xLargest ? used.x : used.y) / 2f + 1.5f;
        }

        // Disable this component as it is done being used.
        enabled = false;
    }

    /// <summary>
    /// Check if a space is not another floor tile so a wall can be placed there.
    /// </summary>
    /// <param name="x">The x coordinate to place a wall at.</param>
    /// <param name="y">The y coordinate to place a wall at.</param>
    private void SpawnWall(int x, int y)
    {
        // Ensure the values are valid just as a failsafe and that this is not a floor.
        if (x < 0 || x >= _size || y < 0 || y >= _size || !_data[x][y])
        {
            Spawn(x, y, wallOffset, wallPrefab, wallOrientation);
        }
    }

    /// <summary>
    /// Set a location as a floor tile.
    /// </summary>
    /// <param name="x">The x coordinate to set as a floor.</param>
    /// <param name="y">The y coordinate to set as a floor.</param>
    protected void SetFloor(int x, int y)
    {
        // Ensure the values are valid just as a failsafe.
        if (x < 0 || x >= _size || y < 0 || y >= _size)
        {
            return;
        }

        // Set it as a floor.
        _data[x][y] = true;
        
        // Check if this is a new largest or smallest x value for visual rendering.
        if (x < _min.x)
        {
            _min.x = x;
        }
        else if (x > _max.x)
        {
            _max.x = x;
        }
        
        // Check if this is a new largest or smallest y value for visual rendering.
        if (y < _min.y)
        {
            _min.y = y;
        }
        else if (y > _max.y)
        {
            _max.y = y;
        }
    }

    /// <summary>
    /// Spawn a prefab.
    /// </summary>
    /// <param name="x">The x position in the generated grid to spawn.</param>
    /// <param name="y">The y position in the generated grid to spawn.</param>
    /// <param name="vertical">The vertical offset of the model to spawn.</param>
    /// <param name="prefab">The prefab to spawn.</param>
    /// <param name="orientation">The orientation of the prefab to spawn.</param>
    private void Spawn(float x, float y, float vertical, GameObject prefab, Vector3 orientation)
    {
        // Center in the world.
        x = x - _min.x - _offset.x;
        y = y - _min.y - _offset.y;
        
        // Spawn the object, set the parent, and set the name.
        GameObject spawned = Instantiate(prefab, new(x, vertical, y), Quaternion.Euler(orientation));
        spawned.transform.parent = transform;
        spawned.name = $"{prefab.name} ({x}, {y})";
    }

    /// <summary>
    /// Random walk logic.
    /// </summary>
    protected abstract void Logic();
}