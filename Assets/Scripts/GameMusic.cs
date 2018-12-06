using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusic : MonoBehaviour
{
    public AudioClip[] _musicClips = new AudioClip[3];
    public AudioClip _gameOverMusic;
    private AudioSource _audiosource;
    private int _songPlaying;
    private int _addValue;

	private void Start ()
    {
        _audiosource = GetComponent<AudioSource>();
        _songPlaying = Random.Range(0, _musicClips.Length);
        _addValue = Random.Range(1, 3);
        _audiosource.clip = _musicClips[_songPlaying];
        _audiosource.Play();
        Invoke("ChangeClips", _audiosource.clip.length);
    }

    private void ChangeClips()
    {
        _songPlaying = (_songPlaying + _addValue) % _musicClips.Length;
        _audiosource.clip = _musicClips[_songPlaying];
        _audiosource.Play();
        Invoke("ChangeClips", _audiosource.clip.length);
    }

    public void SetGameOverMusic()
    {
        _audiosource.Stop();
        _audiosource.clip = _gameOverMusic;
        _audiosource.Play();
    }
}
