using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float _flyforce;
    public float _flyTime;

    private bool _crouched;
    private bool _grounded;
    private bool _flying;
    private float _currentFlyTime;
    private Rigidbody2D _rigidbody;


	private void Start ()
    {
        _grounded = false;
        _flying = false;
        _rigidbody = GetComponent<Rigidbody2D>();
	}
	
	private void Update ()
    {
        _currentFlyTime += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && _grounded)
        {
            _rigidbody.AddForce(new Vector2(0f, _flyforce));

            _grounded = false;
            _flying = true;
        }
        else if (Input.GetKeyUp(KeyCode.Space) || _currentFlyTime >= _flyTime)
        {
            _currentFlyTime = 0f;
            _flying = false;
        }

        if (_flying)
        {
        }
	}

    private void Fly()
    {
        _rigidbody.AddForce(new Vector2(0f, _flyforce * Time.deltaTime));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Platform"))
        {
            _grounded = true;
        }
    }
}
