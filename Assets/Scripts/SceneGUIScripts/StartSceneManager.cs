using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class StartSceneManager : MonoBehaviour
{
    public Button _startGameButton;
    public Button _leaderboardsButton;

    private void Start()
    {
        _startGameButton.onClick.AddListener(StartGameplayScene);
    }

    private void StartLeaderboardsScene()
    { 
    
    }

    private void StartGameplayScene()
    {
        SceneManager.LoadScene("GamePlayScene");
    }
}
