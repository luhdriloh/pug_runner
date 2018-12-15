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
        _leaderboardsButton.onClick.AddListener(StartLeaderboardsScene);
        GoogleLeaderboardsPost.EnableGoogleLeaderBoards();
        GoogleLeaderboardsPost.LogIn();
    }

    private void StartLeaderboardsScene()
    {
        GoogleLeaderboardsPost.ShowLeaderboards();
    }

    private void StartGameplayScene()
    {
        SceneManager.LoadScene("GamePlayScene");
    }
}
