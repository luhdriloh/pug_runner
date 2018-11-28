using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float _jumpVelocity;
    public float _gravityDownMultiplier;

    private bool _crouched;
    private bool _grounded;
    private Rigidbody2D _rigidbody;


	private void Start ()
    {
        _grounded = false;
        _rigidbody = GetComponent<Rigidbody2D>();
	}
	
	private void Update ()
    {
        // TODO: make it so the character cannot rotate when near a ledge 


        if (Input.GetKeyDown(KeyCode.Space) && _grounded)
        {
            _grounded = false;
            Jump();
        }

        if (_rigidbody.velocity.y > 0)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                _rigidbody.velocity -= Vector2.up * _rigidbody.velocity.y;
            }
        }
        else
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
        if (collision.gameObject.layer == LayerMask.NameToLayer("Platform"))
        {
            _grounded = true;
        }
    }
}
