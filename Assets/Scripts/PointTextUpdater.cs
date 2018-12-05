using UnityEngine;
using UnityEngine.UI;

public class PointTextUpdater : MonoBehaviour
{
    private Text _textToUpdate;

	private void Start ()
    {
        _textToUpdate = GetComponent<Text>();
	}
	
	private void Update ()
    {
        _textToUpdate.text = ((long)GameController._gameController._points).ToString();
	}
}
