
using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private LayerMask groundLayer;
    private Rigidbody2D _body;
    private Animator _anim;
    private BoxCollider2D _boxCollider2D;
    

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _body.freezeRotation = true;
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        _body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, _body.velocity.y);

        if (horizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;
        }
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (Input.GetKey(KeyCode.Space) && isGrounded())
        {
            Jump();
        }
        
        _anim.SetBool("run", horizontalInput != 0);
        _anim.SetBool("grounded", isGrounded());
    }

    private void Jump()
    {
        _body.velocity = new Vector2(_body.velocity.x , speed * 1.4f);
        _anim.SetTrigger("jump");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(_boxCollider2D.bounds.center, _boxCollider2D.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
}
