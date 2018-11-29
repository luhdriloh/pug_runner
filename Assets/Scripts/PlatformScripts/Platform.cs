using UnityEngine;

public class Platform : MonoBehaviour
{
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
        return transform.position.x + (_spriteRenderer.size.x * transform.localScale.x) / 2 < -16;
    }

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
