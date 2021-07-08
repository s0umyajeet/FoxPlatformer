using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
        [SerializeField]
        private Rigidbody2D rb;
        [SerializeField]
        private Animator animator;
        [SerializeField]
        private SpriteRenderer sr;
        
        bool isGrounded;
        [SerializeField]
        Transform groundCheck;

        [SerializeField]
        private float playerSpeed = 5f;
        [SerializeField]
        private float playerJump = 4f;
        [SerializeField]
        private int cherries = 0;

        // Start is called before the first frame update
        void Start()        
        {
                rb = GetComponent<Rigidbody2D>();
                animator = GetComponent<Animator>();
                sr = GetComponent<SpriteRenderer>();        
        }       
                

            // Update is called once per frame
        void Update()
        {
                Movement();
                
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
                if (collision.tag == "Collectible")
                {
                        Destroy(collision.gameObject);
                        cherries++;
                }
        }

        void Movement()
        {
                if (Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground")))
                {
                        isGrounded = true;
                }
                else
                {
                        isGrounded = false;
                        animator.Play("player_jump");
                }
           
                float Hdirection = Input.GetAxis("Horizontal");
                if (Hdirection < 0)
                {
                        rb.velocity = new Vector2(-playerSpeed, rb.velocity.y);
                        sr.flipX = true;
                        if (isGrounded)
                            animator.Play("player_run");
                }
                else if (Hdirection > 0)
                {
                        rb.velocity = new Vector2(playerSpeed, rb.velocity.y);
                        sr.flipX = false;
                        if (isGrounded)
                                animator.Play("player_run");
                }
                else
                {
                        rb.velocity = new Vector2(0, rb.velocity.y);
                        if (isGrounded)
                                animator.Play("player_idle");
                }


                if (Input.GetKey("space") && isGrounded)
                {
                        rb.velocity = new Vector2(rb.velocity.x, playerJump);
                        animator.Play("player_jump");
                }
        }
}
