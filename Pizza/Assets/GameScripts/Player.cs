using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    public float jumpForce;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    [SerializeField]
    private int maxJumps;
    public int maxJumpsValue;
    public SpriteRenderer[] Filling;
    private Animator animator;

    void Start()
    {
        maxJumps = maxJumpsValue;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        if (isGrounded)
            maxJumps = maxJumpsValue;
        else if (maxJumps == maxJumpsValue)
            maxJumps--;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && maxJumps > 0)
        {
            
            rb.velocity = Vector2.up * jumpForce;
            maxJumps--;
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0) && maxJumps == 0 && isGrounded)
        {
            
            rb.velocity = Vector2.up * jumpForce;
        }
        if (maxJumps == maxJumpsValue)
        {
            animator.SetBool("isJumping", false);
        }
        else 
            animator.SetBool("isJumping", true);
    }

    public void AddFood(Sprite sprite)
    {
        while (true)
        {
            int i = Random.Range(0, Filling.Length);
            if (Filling[i].sprite == null)
            {
                Filling[i].sprite = sprite;
                break;
            }
        }
    }

    public void Clean()
    {
        for (int i = 0; i < Filling.Length; i++)
            Filling[i].sprite = null;
    }
}
