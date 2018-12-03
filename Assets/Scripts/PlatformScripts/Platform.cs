using UnityEngine;

public class Platform : MonoBehaviour
{
    public int _platformType;
    private SpriteRenderer _spriteRenderer;

    public void PutPlatformIntoPlay(Vector3 position)
    {
        gameObject.SetActive(true);
        transform.position = position;
    }

    public void PutPlatformIntoRest()
    {
        gameObject.SetActive(false);
    }

    public bool OutOfBounds()
    {
        return transform.position.x + (_spriteRenderer.size.x * transform.localScale.x) / 2 < -10;
    }

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
