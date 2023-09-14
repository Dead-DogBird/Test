using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    Rigidbody2D rb;
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    private bool isGrounded = false;

    private float sprayGauge
    {
        get => _sprayGauge;
        set
        {
            _sprayGauge = value;
            UImanager.Instance.SetSprayGauge(_sprayGauge);
        }
    }

    [SerializeField] private GameObject spray;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private float CurGauge;
    private float timer;

    private bool fillGauge;

    [SerializeField] private float _sprayGauge;

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
            if (sprayGauge > 0)
            {
                fillGauge = false;
                sprayGauge -= 0.2f;
                Instantiate(spray,(Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
            }
        }

        if (sprayGauge == CurGauge)
        {
            timer += Time.deltaTime;
        }else{
            CurGauge = sprayGauge;
            timer = 0;
        }
        if (timer > 0.5f)
        {
            fillGauge = true;
        }

        if (fillGauge && sprayGauge < 100)
        {
            sprayGauge += 0.5f;
            if (sprayGauge > 100)
                sprayGauge = 100;
        }
        
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform")||(collision.gameObject.CompareTag("Ground")||
                                                          collision.gameObject.CompareTag("DropedPlatform")||
                                                          collision.gameObject.CompareTag("ColoredPlatform"))&&collision.contacts[1].normal.y>0.7f)
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.transform.CompareTag("DropedPlatform")&&other.transform.position.y<transform.position.y)
        {
            other.transform.GetComponent<DroppedPlatform>().Dropped().Forget();
        }
    }
}
