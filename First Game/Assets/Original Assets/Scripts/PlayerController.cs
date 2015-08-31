using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float moveSpeed;
	public float jumpSpeed;

	private bool grounded;
	public LayerMask whatIsGround;
	public Transform groundCheck;
    public float groundCheckRadius;

	private Animator anim;

    private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator> ();
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

		anim.SetFloat ("Speed", Mathf.Abs(rb.velocity.x));
		anim.SetBool ("InAir", !grounded);

		if (rb.velocity.x > 0) {
			transform.localScale = new Vector3 (1f, 1f, 1f);
		} else if (rb.velocity.x < 0) {
			transform.localScale = new Vector3(-1f, 1f, 1f);
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
