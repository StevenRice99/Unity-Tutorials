using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

/// <summary>
/// The base of our UI that both the menu and game inherit from.
/// This is because for our simple example, both UIs just had a single label and button.
/// In most use cases, you will not likely inherit your UI as all will be vastly different.
/// We require a "UI Document" component, so this ensures one exists in Unity.
/// Slides used in:
/// 5 - User Interfaces
/// </summary>
[RequireComponent(typeof(UIDocument))]
public abstract class BaseUI : MonoBehaviour
{
    /// <summary>
    /// The scene number to load when the button is clicked.
    /// </summary>
    [Tooltip("The scene number to load when the button is clicked.")]
    [SerializeField]
    private int load;

    /// <summary>
    /// The label to display the score in the game and the high score in the menu.
    /// </summary>
    protected Label ScoreLabel;

    /// <summary>
    /// The button to switch between the game and menu.
    /// </summary>
    protected Button LevelButton;

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    protected virtual void Start()
    {
        // Get the component with our UI so we can they query it for the parts we want.
        UIDocument document = GetComponent<UIDocument>();

        // Start from the root of the UI and we can search for the other parts from there.
        VisualElement root = document.rootVisualElement;

        // Get the label for the score and high score.
        ScoreLabel = root.Q<Label>("ScoreLabel");
        
        // Get the button for switching levels.
        LevelButton = root.Q<Button>("LevelButton");

        // Bind the button so every time it is clicked, it calls the "LoadLevel" method.
        LevelButton.clicked += LoadLevel;
    }

    /// <summary>
    /// Load a level.
    /// </summary>
    private void LoadLevel()
    {
        // Load the level based on the index set in the inspector.
        SceneManager.LoadScene(load);
    }

    /// <summary>
    /// Destroying the attached Behaviour will result in the game or Scene receiving OnDestroy.
    /// </summary>
    private void OnDestroy()
    {
        // We need to manually clean up bindings, so as the object is getting destroyed, remove the binding.
        LevelButton.clicked -= LoadLevel;
    }
}