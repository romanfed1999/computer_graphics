using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const string PauseBoxPrefix = "Cardboard Box";
    private const string LavaCube = "Lava Cube 14.02";
    private const string HorizontalAxis = "Horizontal";
    private const string VerticalAxis = "Vertical";
    public float penaltyTime;
    public float speed;
    public float jump;
    private float _timeLeft = 0.0f;
    private bool _onGround = true;
    private Rigidbody _rigidbody;
    private const float Drag = 2.0f;
    private const float ZeroDrag = 0.0f;
    [SerializeField] private float _startPosition = 2.0f;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.drag = 2;
    }

    private void FixedUpdate()
    {
        if (_timeLeft <= 0)
        {
            Move();
            Jump();
        }
        else
        {
            _timeLeft -= Time.deltaTime;
            _rigidbody.velocity = Vector3.zero;
        }
    }

    private void Move()
    {
        float moveHorizontal = Input.GetAxis(HorizontalAxis);
        float moveVertical = Input.GetAxis(VerticalAxis);
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        _rigidbody.AddForce(movement * speed);
    }

    private void Jump()
    {
        if ((!Input.GetButtonDown("Jump")) || !_onGround) return;
        _rigidbody.AddForce(new Vector3(0, jump, 0), ForceMode.Impulse);
        _onGround = false;
        _rigidbody.drag = ZeroDrag;
    }

    private void OnCollisionEnter(Collision collision)
    {
        print(collision.gameObject.name);
        ChangeCollisionState(true, collision);
    }

    private void ChangeCollisionState(bool isOnGround, Collision collision)
    {
        _onGround = isOnGround;
        _rigidbody.drag = Drag;
        if (collision.gameObject.name.StartsWith(PauseBoxPrefix))
        {
            _timeLeft = penaltyTime;
            _rigidbody.velocity = Vector3.zero;
        }

        if (collision.gameObject.name != LavaCube) return;
        _timeLeft = penaltyTime;
        _rigidbody.velocity = Vector3.zero;
        transform.position = new Vector3(0, _startPosition, 0);
    }
}