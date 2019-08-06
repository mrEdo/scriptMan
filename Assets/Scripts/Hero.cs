using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using RigidBody2D;

public class Hero : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _sprite;

    private Rigidbody2D _rigid;
    [SerializeField]
    private float _jumpForce = 5.0f;
    [SerializeField]
    private bool _grounded = false;
    [SerializeField]
    private LayerMask _groundLayer;
    private bool resetJumpNeeded;
    [SerializeField]
    private float speed = .3f;

    private Animator _anim;

    // Start is called before the first frame update
    void Start()
    {
       _rigid = GetComponent<Rigidbody2D>();
       _sprite = GetComponentInChildren<SpriteRenderer>();
       _anim = _anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update(){
        Movement();
    }
  
    void Movement (){
        float horizontalInput = UnityEngine.Input.GetAxisRaw("Horizontal");
        
        if( horizontalInput > 0){
            _sprite.flipX = true;
        } else {
            _sprite.flipX = false;
        }

        if( horizontalInput == 1 || horizontalInput == -1 ){
            _anim.SetBool("running", true);
        } else {
            _anim.SetBool("running", false);
        }
        
        
        _grounded = false;
        isGrounded();
        _anim.SetBool("jumping", _grounded);
        
        _rigid.velocity = new Vector2(horizontalInput * speed, _rigid.velocity.y);
        if(UnityEngine.Input.GetKeyDown(KeyCode.Space) && _grounded == true){
            _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce);
            // _grounded = false;
            resetJumpNeeded = true;
            StartCoroutine(ResetJumpNeedRoutine());
        }
    }
    
    void isGrounded(){

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 1.0f, _groundLayer);
        Debug.DrawRay(transform.position, Vector2.down * 0.6f, Color.green);

        if(hitInfo.collider != null){
            Debug.Log("You hit " + hitInfo.collider.name);
            _grounded = true;
        } 
    }
    IEnumerator ResetJumpNeedRoutine(){
        resetJumpNeeded = true;
        yield return new WaitForSeconds(0.1f);
        resetJumpNeeded = false;
    }
    
}
