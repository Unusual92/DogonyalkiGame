using UnityEngine;
using UnityEngine.UI;

public class CloseGame : MonoBehaviour
{
    public Button closeButton;

    private void Start()
    {
        closeButton.onClick.AddListener(CloseGameButton);
    }

    private void CloseGameButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}