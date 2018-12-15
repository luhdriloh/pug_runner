using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


public class PlayerSaveData : MonoBehaviour
{
    public static PlayerSaveData _playerSaveDataInstance;
    public int _lastPlaceAttained;
    private HighscoreSaveData _highscores;
    private readonly string _saveFileName = "/foxy_runner_highscores.dat";
    private long _currentHighscore;

    private void Awake()
    {
        if (_playerSaveDataInstance == null)
        {
            _playerSaveDataInstance = this;
        }
        else if (this != _playerSaveDataInstance)
        {
            Destroy(this);
        }

        _lastPlaceAttained = 0;
        Load();
    }

    private void Load()
    {
        if (File.Exists(Application.persistentDataPath + _saveFileName))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = File.Open(Application.persistentDataPath + _saveFileName, FileMode.Open);
            _highscores = (HighscoreSaveData)binaryFormatter.Deserialize(fileStream);
            fileStream.Close();
        }
        else
        {
            // create data for just one level
            _highscores = new HighscoreSaveData();
            _highscores._listOfHighscores = new List<long>();
            for (int i = 0; i < 10; i++)
            {
                _highscores._listOfHighscores.Add(0);
            }

            Save();
        }

        int numberOfScoreValues = _highscores._listOfHighscores.Count;
        if (numberOfScoreValues < 10)
        {
            for (int i = 0; i < 10 - numberOfScoreValues; i++)
            {
                _highscores._listOfHighscores.Add(0);
            }
        }
    }

    private void Save()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = File.Open(Application.persistentDataPath + _saveFileName, FileMode.Create);

        binaryFormatter.Serialize(fileStream, _highscores);
        fileStream.Close();
    }

    public List<long> GetHighscores()
    {
        return _highscores._listOfHighscores;
    }

    public void UpdateHighscore(float highscore)
    {
        int scoreGreaterThanHowManyPreviousOnes = 0;

        foreach (long score in _highscores._listOfHighscores)
        {
            if (score < highscore)
            {
                scoreGreaterThanHowManyPreviousOnes++;
            }
        }

        _lastPlaceAttained = 10 - scoreGreaterThanHowManyPreviousOnes;

        if (scoreGreaterThanHowManyPreviousOnes > 0)
        {
            _highscores._listOfHighscores.Add((long)highscore);
            _highscores._listOfHighscores.Sort();

            _highscores._listOfHighscores = _highscores._listOfHighscores.GetRange(1, 10);
            Save();
        }
    }
}
