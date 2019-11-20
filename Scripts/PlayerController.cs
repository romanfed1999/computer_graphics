using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

  public float penaltyTime;
  public float speed;
  public float jump;
  private float _timeLeft = 0.0f;
  private bool _onGround = true;
  private Rigidbody _rb;

  private void Start()
  {
    _rb = GetComponent<Rigidbody>();
    _rb.drag = 2;
  }

  private void FixedUpdate()
  {
    if(_timeLeft <= 0)
    {
      Move();
      Jump();
    }
    else{
      _timeLeft -= Time.deltaTime;
      _rb.velocity = Vector3.zero;
    }
  }

  private void Move()
  {
    float moveHorizontal = Input.GetAxis("Horizontal");
    float moveVertical = Input.GetAxis("Vertical");
    Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

    _rb.AddForce(movement * speed);
  }

  private void Jump()
  {
    if ((!Input.GetButtonDown("Jump")) || !_onGround) return;
    _rb.AddForce(new Vector3(0, jump, 0), ForceMode.Impulse);
    _onGround = false;
    _rb.drag = 0;
  }

  private void OnCollisionEnter(Collision collision)
  {
    print(collision.gameObject.name);
    ChangeCollisionState(true, collision);
  }

  private void  ChangeCollisionState(bool isOnGround, Collision collision)
  {
    this._onGround = isOnGround;
    _rb.drag = 2;
    if ((collision.gameObject.name == "Cardboard Box 02.02") || (collision.gameObject.name == "Cardboard Box 14.10"))
    {
      _timeLeft = penaltyTime;
      _rb.velocity = Vector3.zero;
    }

    if (collision.gameObject.name != "Lava Cube 14.02") return;
    _timeLeft = penaltyTime;
    _rb.velocity = Vector3.zero;
    transform.position = new Vector3(0, 2.0f, 0);
  }
}
