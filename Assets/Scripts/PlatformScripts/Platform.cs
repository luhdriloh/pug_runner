using UnityEngine;

public class Platform : MonoBehaviour
{
    public int _platformType;
    private SpriteRenderer _spriteRenderer;
    private float _platformWidth;

    public void PutPlatformIntoPlay(Vector3 position)
    {
        gameObject.SetActive(true);
        transform.position = position;
    }

    public void PutPlatformIntoRest()
    {
        gameObject.SetActive(false);
    }

    public float GetPlatformWidth()
    {
        return _platformWidth;
    }

    public float GetFarRightSide()
    {
        return transform.position.x + _platformWidth / 2;
    }

    public bool OutOfBounds()
    {
        return transform.position.x + _platformWidth / 2 < -10;
    }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _platformWidth = _spriteRenderer.bounds.size.x;
        gameObject.SetActive(false);
    }
}
