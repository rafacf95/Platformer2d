using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    public ProjectileBase prefabProjectile;
    public Transform positionToShoot;
    public Transform playerDirectionReference;
    // Start is called before the first frame update
    public void Shoot()
    {
        var projectile = Instantiate(prefabProjectile);
        projectile.transform.position = positionToShoot.position;
        projectile.side = playerDirectionReference.transform.localScale.x;
    }

    void Awake()
    {
        playerDirectionReference = GetComponentInParent<Player>().gameObject.transform;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Shoot();
        }
    }
}
