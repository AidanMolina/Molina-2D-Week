using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{

    Rigidbody2D rigidBody2D;

    public float runSpeed;
    public float jumpSpeed;
    private int count;

    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public TextMeshProUGUI countText;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        count = 0;

        SetCountText();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump")){
            int levelMask = LayerMask.GetMask("Level");
            if(Physics2D.BoxCast(transform.position, new Vector2(1f, .1f), 0f, Vector2.down, 0.1f, levelMask)){
                Jump();
            }
            
        }

    }

    void FixedUpdate(){
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector2 direction = new Vector2(horizontalInput * runSpeed * Time.deltaTime, 0f);

        rigidBody2D.velocity = new Vector2(horizontalInput * runSpeed *Time.deltaTime, rigidBody2D.velocity.y);

        if(rigidBody2D.velocity.x >= 0){
            spriteRenderer.flipX = false;
        }
        else{
            spriteRenderer.flipX = true;
        }

        if(Mathf.Abs(horizontalInput) > 0f){
            animator.SetBool("isRunning", true);
        }
        else{
            animator.SetBool("isRunning", false);
        }

    }

    void Jump(){
        rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, jumpSpeed);
    }

    void SetCountText(){
        countText.text = "Score: " + count.ToString();
    }

    void OnTriggerEnter(Collider other){
        Debug.Log("Yes");
        if(other.gameObject.CompareTag("Pickup")){
            other.gameObject.SetActive(false);
            count++;

            SetCountText();

            Debug.Log("Yes");
        }
    }
}
