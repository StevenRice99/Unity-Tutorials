using Unity.Mathematics;
using UnityEngine;

/// <summary>
/// A simple class to display the frames per second to the screen.
/// Slides used in:
/// 11 - Singleton Pattern
/// </summary>
public class FramesCounter : MonoBehaviour
{
    /// <summary>
    /// OnGUI is called for rendering and handling GUI events.
    /// </summary>
    private void OnGUI()
    {
        // A slight padding so the numbers don't render on the edge of visibility.
        const int padding = 10;
        
        // Increase the font size.
        GUI.skin.label.fontSize = GUI.skin.box.fontSize = GUI.skin.button.fontSize = 72;
        
        // Render the frames per second.
        GUI.Label(new(padding, padding, Screen.width - padding, Screen.height - padding), $"{math.round(1 / Time.deltaTime)}");
    }
}