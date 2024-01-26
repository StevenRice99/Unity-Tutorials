using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// The UI for our game to display the score and a button to go to the menu.
/// Slides used in:
/// 5 - User Interfaces
/// </summary>
public class GameUI : BaseUI
{
    /// <summary>
    /// Track the score in the "game", which in this demonstration is just how many times we hit the space button.
    /// </summary>
    private int _score;

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    protected override void Start()
    {
        // Call the base start method so the label and button are set up first.
        base.Start();

        // Display no score at the start of the game.
        ScoreLabel.text = "Score = 0";
        
        // This button is for taking us to the menu so make it say so.
        LevelButton.text = "Menu";
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    private void Update()
    {
        // Do nothing if space is not pressed.
        if (!Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            return;
        }

        // Increment the score when the space key is pressed.
        _score++;

        // Update the label to display the new score.
        ScoreLabel.text = $"Score = {_score}";

        // Check if the current score is greater than the existing high score.
        if (_score > PlayerPrefs.GetInt("HIGH SCORE"))
        {
            // If it is greater, set the current score as the new high score.
            PlayerPrefs.SetInt("HIGH SCORE", _score);
        }
    }
}