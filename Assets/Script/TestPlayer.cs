using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    Rigidbody2D rb;
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    private bool isGrounded = false;
    [SerializeField] private GameObject spray;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    { 
        transform.Translate(new Vector3(moveSpeed*Input.GetAxisRaw("Horizontal"), 0) * Time.deltaTime);
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jumpForce);
            isGrounded = false;
        }

        if (Input.GetMouseButton(0))
        {
            Instantiate(spray,(Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
        }
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if ((collision.gameObject.CompareTag("Ground")||collision.gameObject.CompareTag("DropedPlatform")||collision.gameObject.CompareTag("ColoredPlatform"))&&collision.contacts[1].normal.y>0.7f)
        {
            isGrounded = true;
        }
    }
}
