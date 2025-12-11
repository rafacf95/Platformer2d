using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    [Header("Setup")]
    public ProjectileBase prefabProjectile;
    public Transform positionToShoot;
    public Transform playerDirectionReference;
    public AudioRandomPlayAudioClips shootSfx;
    // Start is called before the first frame update
    public void Shoot()
    {
        var projectile = Instantiate(prefabProjectile);
        projectile.transform.position = positionToShoot.position;
        projectile.side = playerDirectionReference.transform.localScale.x;
        shootSfx.PlayRandom();
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
