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
        // 점프
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector2.up * jumpForce);
            isGrounded = false;
        }

        if (Input.GetMouseButton(0))
        {
            Instantiate(spray, transform.position, Quaternion.identity);
        }
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        // 바닥에 닿으면 isGrounded를 true로 설정
        if (collision.gameObject.CompareTag("Ground")&&collision.contacts[1].normal.y>0.7f)
        {
            isGrounded = true;
        }
    }
}
