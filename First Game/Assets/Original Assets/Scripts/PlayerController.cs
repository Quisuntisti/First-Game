using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float moveSpeed;
	public float jumpSpeed;

	private bool grounded;
	public LayerMask whatIsGround;
	public Transform groundCheck;
    public float groundCheckRadius;

    private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Space) && grounded  ) {
			rb.velocity = new Vector2 (rb.velocity.x, jumpSpeed);
		}

		if (Input.GetKey (KeyCode.D)) {
			rb.velocity = new Vector2 (moveSpeed, rb.velocity.y);
		}

		if (Input.GetKey (KeyCode.A)) {
			rb.velocity = new Vector2 (-moveSpeed, rb.velocity.y);
		}
	}

	void FixedUpdate(){
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, whatIsGround);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "spike")
        {
            Debug.Log("Dead");
            Application.LoadLevel("Level 1");
        }


    }

}
