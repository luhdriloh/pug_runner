using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float _jumpVelocity;
    public float _gravityDownMultiplier;

    private float _xStart;

    private PlayerState _playerState;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private GameObject _lastTouchedGameObject;

    private enum PlayerState { UNKNOWN, RUNNING, CROUCHING, JUMPING, FALLING };

	private void Start ()
    {
        _xStart = transform.position.x;

        _playerState = PlayerState.RUNNING;
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _lastTouchedGameObject = null;
    }


    private void FixedUpdate()
    {
        if (_rigidbody.velocity.y <= 0)
        {
            _rigidbody.AddForce(Vector2.up * (_gravityDownMultiplier * Physics2D.gravity * _rigidbody.mass * Time.deltaTime));
        }
    }


    private void Update ()
    {
        switch (_playerState)
        {
            case PlayerState.RUNNING:
                // jumping
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _playerState = PlayerState.JUMPING;
                    Jump();
                }

                break;

            case PlayerState.JUMPING:
                if (_rigidbody.velocity.y > 0)
                {
                    if (Input.GetKeyUp(KeyCode.Space))
                    {
                        _animator.SetBool("Jump", false);

                        _rigidbody.velocity -= Vector2.up * _rigidbody.velocity.y;
                    }
                }
                else if (_rigidbody.velocity.y <= -Mathf.Epsilon)
                {
                    _playerState = PlayerState.FALLING;
                    Falling();
                }
                break;

            case PlayerState.FALLING:

                break;

            default:
                break;
        }

        transform.position = new Vector3(_xStart, transform.position.y, transform.position.z);

        if (!InView() && !GameController._gameController._gameOver)
        {
            GameController._gameController.GameOver();
        }
    }


    private bool InView()
    {
        return transform.position.y > -7;
    }


    private void Falling()
    {
        _animator.SetTrigger("Top");
        _animator.SetBool("Jump", false);
        _animator.SetBool("Falling", true);
    }


    private void Jump()
    {
        _animator.SetBool("Jump", true);
        _rigidbody.velocity += Vector2.up * _jumpVelocity;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Platform") && (_playerState == PlayerState.FALLING || _playerState == PlayerState.UNKNOWN))
        {
            if (collision.gameObject != _lastTouchedGameObject)
            {
                GameController._gameController.Score();
                _lastTouchedGameObject = collision.gameObject;
            }

            _animator.SetBool("Falling", false);
            _animator.SetTrigger("Running");
            _playerState = PlayerState.RUNNING;
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Platform") && (_playerState == PlayerState.RUNNING))
        {
            _animator.SetBool("Falling", true);
            _playerState = PlayerState.FALLING;
        }
    }
}
