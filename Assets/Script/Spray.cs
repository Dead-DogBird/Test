using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Random = UnityEngine.Random;

public class Spray : MonoBehaviour
{
    private CircleCollider2D my_collider;

    private bool isColiderCheck = false;

    private int id;
    // Start is called before the first frame update
    void Start()
    {
        my_collider = GetComponent<CircleCollider2D>();
        AutoDestroy().Forget();
        id = Random.Range(0, 100);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.64f*0.5f);
    }
    // Update is called once per frame
    void Update()
    {
       // if(isColiderCheck)
       // CheckSpray();
    }

    public void CancleDestroyCallback(Transform _transform)
    {
        transform.parent = _transform;
        isColiderCheck = true;
        Destroy(GetComponent<CircleCollider2D>());
    }
    void CheckSpray()
    {
        
    }

    private void OnDestroy()
    {
        isColiderCheck = true;
    }

    async UniTaskVoid AutoDestroy()
    {
        for (int i = 0; i < 15; i++)
        {
            if (isColiderCheck)
            {
                return;
            }
            await UniTask.Delay(TimeSpan.FromSeconds(0.1f));
        }
        Destroy(gameObject);
    }
    
}
