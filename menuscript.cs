using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public Button offlineButton;
    public Button onlineButton;
    public Button exitButton;

    private void Start()
    {
        offlineButton.onClick.AddListener(OnOfflineButtonClick);
        onlineButton.onClick.AddListener(OnOnlineButtonClick);
        exitButton.onClick.AddListener(OnExitButtonClick);
    }

    private void OnOfflineButtonClick()
    {
        SceneManager.LoadScene("OfflineScene");
    }

    private void OnOnlineButtonClick()
    {
        SceneManager.LoadScene("OnlineScene");
    }

    private void OnExitButtonClick()
    {
        Application.Quit();
    }
}
