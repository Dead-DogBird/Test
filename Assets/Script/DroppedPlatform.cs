using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DroppedPlatform : MonoBehaviour
{
    private SpriteRenderer _sprite;
    private Tween _tween;

    private float gravity = 0.05f;

    private bool isDrop = false;
    // Start is called before the first frame update
    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        if(isDrop)
            Falling();
    }

    private void OnDestroy()
    {
        _tween.Complete();
    }

    void Falling()
    {
        gravity += gravity * 0.02f;
        transform.position += new Vector3(0, gravity);
    }
    public void Dropped()
    {
        isDrop = true;
        _tween =  _sprite.DOColor(new Color(40/255f,36/255f,90/255f), 0.2f);
        Destroy(gameObject,1.2f);
    }
    
}
