using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float _jumpVelocity;
    public float _gravityDownMultiplier;

    private PlayerState _playerState;
    private Rigidbody2D _rigidbody;
    private CircleCollider2D _circleCollider;

    private enum PlayerState { UNKNOWN, GROUNDED, CROUCHED, JUMPING };

	private void Start ()
    {
        _playerState = PlayerState.UNKNOWN;
        _rigidbody = GetComponent<Rigidbody2D>();
        _circleCollider = GetComponent<CircleCollider2D>();
	}
	
	private void Update ()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) && _playerState != PlayerState.JUMPING)
        {
            _playerState = PlayerState.CROUCHED;

            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y / 2, 0);
            _circleCollider.radius = _circleCollider.radius / 2;
        }

        if (Input.GetKeyUp(KeyCode.DownArrow) && _playerState == PlayerState.CROUCHED)
        {
            Debug.Log("up from crouch");
            _playerState = PlayerState.GROUNDED;

            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * 2, 0);
            _circleCollider.radius = _circleCollider.radius * 2;
        }


        if (Input.GetKeyDown(KeyCode.Space) && _playerState != PlayerState.JUMPING)
        {
            _playerState = PlayerState.JUMPING;
            Jump();
        }

        if (_rigidbody.velocity.y > 0)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                _rigidbody.velocity -= Vector2.up * _rigidbody.velocity.y;
            }
        }
        else if (_rigidbody.velocity.y < 0)
        {
            _rigidbody.AddForce(Vector2.up * (_gravityDownMultiplier * Physics2D.gravity * _rigidbody.mass * Time.deltaTime)); // times the mass of the object
        }
    }

    private void Jump()
    {
        _rigidbody.velocity += Vector2.up * _jumpVelocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Platform") && _playerState == PlayerState.JUMPING)
        {
            _playerState = PlayerState.GROUNDED;
        }
    }
}
