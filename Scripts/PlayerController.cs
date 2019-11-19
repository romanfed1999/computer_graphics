using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
  
  public float speed;
  public float jump;
  public float timeLeft = 0.0f;
  private bool onGround = true;
  private Rigidbody rb;

  void Start()
  {
    rb = GetComponent<Rigidbody>();
  }

  void FixedUpdate()
  {
    if(timeLeft <= 0)
    {
      float moveHorisontal = Input.GetAxis("Horizontal");
      float moveVertical = Input.GetAxis("Vertical");
      Vector3 movement = new Vector3(moveHorisontal, 0.0f, moveVertical);

      rb.AddForce(movement * speed);
      Jump();
    }
    else{
      timeLeft -= Time.deltaTime;
      rb.velocity = Vector3.zero;
    }
  }
  
  private void Jump()
  {
      if ((Input.GetButtonDown("Jump")) && onGround)
      { 
        rb.AddForce(new Vector3(0, jump, 0), ForceMode.Impulse);
        onGround = false;
      }
  }

  private void OnCollisionEnter(Collision collision)
  {
    print(collision.gameObject.name);
    ChangeCollisionState(true, collision);
  }

  public void  ChangeCollisionState(bool onGround, Collision collision)
  {
    this.onGround = onGround;
    if ((collision.gameObject.name == "Cardboard Box 02.02") || (collision.gameObject.name == "Cardboard Box 14.10"))
    {
      timeLeft = 3.0f;
      rb.velocity = Vector3.zero;
    }

    if (collision.gameObject.name == "Lava Cube 14.02")
    {
      timeLeft = 3.0f;
      rb.velocity = Vector3.zero;
      transform.position = new Vector3(0, 2.0f, 0);
    }
  }
}
