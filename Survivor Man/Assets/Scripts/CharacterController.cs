using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    float horizontalInput;
    private float moveSpeed = 4;
    private float jumpForce = 12;
    public bool isGrounded = true;

    private Rigidbody2D playerRb;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        anim = this.transform.Find("Player").GetComponent<Animator>();
        Flip(false);
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        anim.SetFloat("Speed", Mathf.Abs(horizontalInput));

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            anim.Play("Jump");
            playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
            anim.SetBool("isGrounded", true);
        }

        // Change look direction
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
            Flip(true);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            Flip(false);
        }
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector2.right * horizontalInput * moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }


    // Decides which direction the swordsman should look
    void Flip(bool lookLeft)
    {
        transform.localScale = new Vector3(lookLeft ? 1 : -1, 1, 1);
    }


}
