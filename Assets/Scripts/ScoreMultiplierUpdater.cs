using UnityEngine;
using UnityEngine.UI;

public class ScoreMultiplierUpdater : MonoBehaviour
{
    private Text _textToUpdate;

    private void Start()
    {
        _textToUpdate = GetComponent<Text>();
    }

    private void Update()
    {
        _textToUpdate.text = "x " + ((long)GameController._gameController._speedUpMultiplier).ToString();
    }
}
