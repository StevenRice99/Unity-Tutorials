using UnityEngine;

/// <summary>
/// The UI for our menu to display the high score and a button to play the game.
/// Slides used in:
/// 5 - User Interfaces
/// </summary>
public class MenuUI : BaseUI
{
    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    protected override void Start()
    {
        // Call the base start method so the label and button are set up first.
        base.Start();

        // Display the high score.
        ScoreLabel.text = $"High Score = {PlayerPrefs.GetInt("HIGH SCORE")}";
        
        // This button is for playing the game so make it say so.
        LevelButton.text = "Play";
    }
}