using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public bool debugMode;
    public string color;
    public bool flipAtStart = false;
    public Camera mainCamera;
    public LayerMask platformLayerMask;

    private bool grounded = false;
    private bool normalGravity = true;
    private float posYAtJump = 0.0f;
    private float lastTouchedPlatformPosY = 0.0f;
    private Rigidbody2D rigidBody2D;
    private BoxCollider2D boxCollider2D;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
    }

    public void Restart(string color)
    {
        this.color = color;
        mainCamera = GameObject.FindObjectOfType<Camera>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        initGravitySetting(flipAtStart);
    }
    void FixedUpdate()
    {
        debugMode = ApplicationModel.debugMode;

        Move(debugMode);
        if ((Input.GetAxis("gravityToggle") > 0.05f))
        {
            ToggleDirection();
        }
        //if (normalGravity)
        //{
        //    RaycastHit2D hit = Physics2D.Raycast(this.transform.position, new Vector2(0, -1.0f),1.0f, platformLayerMask, -1.2f, -0.95f);
        //    Debug.Log(hit.collider.name);
        //    Debug.DrawRay(this.transform.position, hit.point);
        //    if (hit.collider != null && hit.distance <= 0.1)
        //        grounded = true;
        //    else
        //        grounded = false;
        //}
        //else
        //{
        //    RaycastHit2D hit = Physics2D.Raycast(this.transform.position, new Vector2(0, 1.0f),1.0f, platformLayerMask, - 1.2f, -0.95f);
        //    Debug.Log(hit.collider.name);
        //    Debug.DrawRay(this.transform.position, hit.point);
        //    if (hit.collider != null && hit.distance <= 0.1) //&& hit.collider.gameObject != gameObject
        //        grounded = true;
        //    else
        //        grounded = false;
        //}
    }

    private void Move(bool debugMode)
    {
        float x;
        if (debugMode)
        {
            x = Input.GetAxisRaw("Horizontal");
        }
        else
        {
            x = 2 * ApplicationModel.difficulty + calcVelocityCoeffBasedOnCameraPosition();
        }
        float moveBy = x;
        rigidBody2D.velocity = new Vector2(moveBy, rigidBody2D.velocity.y);
        animator.SetFloat("runningSpeed", Mathf.Abs(moveBy));
        if (moveBy < 0.0f)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
    public void ToggleDirection()
    {
        if (grounded)
        {
            if (normalGravity)
            {
                rigidBody2D.gravityScale = -1.0f;
                this.transform.position += new Vector3(0.0f, 0.1f, 0.0f);
                normalGravity = false;
                animator.SetTrigger("Jump");
                spriteRenderer.flipY = true;
                grounded = false;
            }
            else
            {
                rigidBody2D.gravityScale = 1.0f;
                this.transform.position += new Vector3(0.0f, -0.1f, 0.0f);
                normalGravity = true;
                animator.SetTrigger("Jump");
                spriteRenderer.flipY = false;
                grounded = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if ((other.collider.tag == "Player"   ||
            other.collider.tag == "Platform") && ((normalGravity && (other.transform.position.y <= this.transform.position.y))
                                             || (!normalGravity && (other.transform.position.y >= this.transform.position.y)))
           )
        {
            if (Mathf.Abs(posYAtJump - this.transform.position.y) >= 0.8f || Mathf.Abs(lastTouchedPlatformPosY - other.transform.position.y) >= 1.8f)
                animator.SetTrigger("Land");
            grounded = true;
            if (!normalGravity && other.transform.position.z != -1.15f) // platform blokkok esztetikai celu mozgatasa...
            {
                other.transform.position += new Vector3(0.0f, 0.0f, -0.15f);
            }
            else if (normalGravity && other.transform.position.z != -1.0f)
            {
                other.transform.position += new Vector3(0.0f, 0.0f, 0.15f);
            }                                                           // /platform blokkok esztetikai celu mozgatasa...
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.tag == "Platform")
        {
            lastTouchedPlatformPosY = other.transform.position.y;
            posYAtJump = this.transform.position.y;
            //animator.SetTrigger("Jump");
            //if (!boxCollider2D.IsTouchingLayers(9))
            //{
            //    grounded = false;
            //}
        }
    }
    private float calcVelocityCoeffBasedOnCameraPosition()
    {
        float playerPosX = this.transform.position.x;
        float cameraPosX = mainCamera.transform.position.x;
        float difference = (cameraPosX - playerPosX) / 5.0f;
        return difference;
    }



    public void initGravitySetting(bool normal)
    {
        if (normal)
        {
            spriteRenderer.flipY = false;
            rigidBody2D.gravityScale = 1.0f;
            normalGravity = true;
        }
        else
        {
            spriteRenderer.flipY = true;
            rigidBody2D.gravityScale = -1.0f;
            normalGravity = false;
        }
    }
}
