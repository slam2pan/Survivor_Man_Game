using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    float horizontalInput;
    private float moveSpeed = 4;
    private float jumpForce = 12;
    private bool isGrounded = true;
    private int maxHealth = 5;
    private int health = 5;
    private int hitRange = 1;

    private float timeInvincible = 2.0f;
    private bool isInvincible = false;
    private float invincibleTimer;

    private Rigidbody2D playerRb;
    private Animator anim;
    [SerializeField] TextMeshProUGUI healthText;

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
            anim.SetBool("isGrounded", true);
        }

        // Crouching
        if (Input.GetKey(KeyCode.S))
        {
            anim.SetBool("isCrouching", true);
        } else
        {
            anim.SetBool("isCrouching", false);
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

        // Attack
        if (Input.GetKeyDown(KeyCode.E))
        {
            anim.SetTrigger("isAttacking");
        }
        // Continuous attack damage
        if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            Attack();
        }

        // Invincible timer to taking damage (need to add animation)
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
            {
                isInvincible = false;
            }
        }
    }

    private void FixedUpdate()
    {
        // Don't allow character to move if he is crouching
        if (!anim.GetBool("isCrouching"))
        {
            transform.Translate(Vector2.right * horizontalInput * moveSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Damage"))
        {
            ChangeHealth(-1);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    // Decides which direction the swordsman should look
    void Flip(bool lookLeft)
    {
        transform.localScale = new Vector3(lookLeft ? 1 : -1, 1, 1);
    }

    private void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
            {
                return;
            }
            isInvincible = true;
            invincibleTimer = timeInvincible;
        }

        health = Mathf.Clamp(health + amount, 0, maxHealth);
        healthText.SetText("Health: " + health);
    }

    public int GetHealth()
    {
        return health;
    }

    // Destroy object if player hits it
    private void Attack()
    {
        RaycastHit2D hit;
        LayerMask layerMask = 9; // ignore player
        Vector2 forward = new Vector2(-transform.localScale.x, 0);
        Vector2 origin = transform.position;

        if (hit = Physics2D.Raycast(origin, forward, hitRange, layerMask))
        {
            Debug.Log(hit.transform.gameObject);
            if (hit.transform.gameObject.CompareTag("Damage"))
            {
                Destroy(hit.transform.gameObject);
            }
        }
    }
}
