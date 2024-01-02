using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;
    private CapsuleCollider2D capsuleCollider;

    [SerializeField] private LayerMask jumpableGround;

    private float dirX;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;
    
    public bool OnGround { get; private set; }
    public bool OnWall { get; private set; }
    public Vector2 ContactNormal { get; private set; }

    private enum MovementState
    {
        idle,
        running,
        jumping,
        falling,
        hanging
    };

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
        
        if (Input.GetButtonDown("Jump") && (OnGround || OnWall))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        AnimationUpdate();
    }

    private void AnimationUpdate()
    {
        MovementState state;
        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }
        if (rb.velocity.y>.1f)
        {
            state = MovementState.jumping;
        }
        else if(rb.velocity.y<-.1f)
        {
            state = MovementState.falling;
        }

        if (OnWall && rb.velocity.y < .1f) 
        {
            state = MovementState.hanging;
        }
        anim.SetInteger("state",(int)state);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        OnGround = false;
        OnWall = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EvaluateCollision(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        EvaluateCollision(collision);
    }

    public void EvaluateCollision(Collision2D collision)
    {
        for (int i = 0; i < collision.contactCount; i++)
        {
            ContactNormal = collision.GetContact(i).normal;
            OnGround |= ContactNormal.y >= 0.1f;
            OnWall = Mathf.Abs(ContactNormal.x) >= 0.9f;
        }
    }
    
    /*private bool IsGrounded()
    {
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }*/
}
