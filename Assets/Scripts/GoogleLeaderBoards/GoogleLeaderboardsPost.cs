using System.Collections.Generic;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class GoogleLeaderboardsPost : MonoBehaviour
{
    public int _googleLeaderboardsFailureRetries;
    private static readonly string _foxyRunnerLeaderBoardsId = "CgkInNiuuLgWEAIQAA";

    public static void EnableGoogleLeaderBoards()
    {
        // recommended for debugging:
        PlayGamesPlatform.DebugLogEnabled = true;

        // Activate the Google Play Games platform
        PlayGamesPlatform.Activate();
    }

    public static void PostScoreToGoogleLeaderboards(long score)
    {
        if (Social.localUser.authenticated)
        {
            Social.ReportScore(score, _foxyRunnerLeaderBoardsId, (bool success) =>
            {
                if (success)
                {
                    Debug.Log("Score reported");
                }
                else
                {
                    Debug.Log("Score not reported");
                }
            });
        }
    }

    public static void ShowLeaderboards()
    {
        if (Social.localUser.authenticated)
        {
            Social.ShowLeaderboardUI();
        }
    }


    public static void LogIn()
    {
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                Debug.Log("Login Sucess");
            }
            else
            {
                Debug.Log("Login failed");
            }
        });
    }
}


