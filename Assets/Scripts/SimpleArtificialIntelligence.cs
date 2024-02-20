using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// A basic artificial intelligence that navigates towards the player.
/// Slides used in:
/// 13 - Artificial Intelligence
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public class SimpleArtificialIntelligence : MonoBehaviour
{
    /// <summary>
    /// The agent to use for navigating.
    /// </summary>
    [HideInInspector]
    [SerializeField]
    private NavMeshAgent agent;
    
    /// <summary>
    /// The transform of the player to navigate to.
    /// </summary>
    [HideInInspector]
    [SerializeField]
    private Transform target;
    
    /// <summary>
    /// Editor-only function that Unity calls when the script is loaded or a value changes in the Inspector.
    /// </summary>
    private void OnValidate()
    {
        // Check if there is no agent.
        if (agent == null)
        {
            // Assign the agent if it is not yet.
            agent = GetComponent<NavMeshAgent>();
        }
        
        // If there is already the player target, do nothing.
        if (target != null)
        {
            return;
        }

        // Find the player.
        TopDownCharacterGlobal player = FindObjectOfType<TopDownCharacterGlobal>();
        
        // Ensure there was a player that was found.
        if (player != null)
        {
            // Assign the transform of the player if the player was found in the scene.
            target = player.transform;
        }
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    private void Update()
    {
        // Set the pathfinding to go to the player's position.
        agent.destination = target.position;
    }
}