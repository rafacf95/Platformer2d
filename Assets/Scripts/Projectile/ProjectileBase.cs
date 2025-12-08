using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public Vector3 direction;
    public float timeToDestroy = 1f;
    public float side = 1;
    public int damage = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("colide");
        var enemy = collision.transform.GetComponent<EnemyBase>();
        
        if(enemy != null)
        {
            enemy.Damage(damage);
            Destroy(gameObject);
        }
    }

    void Awake()
    {
        Destroy(gameObject, timeToDestroy);
    }
    void Update()
    {
        transform.Translate(direction * Time.deltaTime * side);
    }
}
