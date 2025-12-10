using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableBase : MonoBehaviour
{
    [Header("Configuration")]
    public string compareTag = "Player";
    public ParticleSystem particles;
    public float timeToHide = 3f;
    public GameObject graphItem;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag(compareTag))
        {
            Collect();
        }
    }

    private void HideObject()
    {
        gameObject.SetActive(false);
    }

    protected virtual void Collect()
    {
        Debug.Log("Collect");
        if(graphItem != null) graphItem.SetActive(false);
        Invoke(nameof(HideObject), timeToHide);
        OnCollect();
    }

    protected virtual void OnCollect()
    {
        Debug.Log("OnCollect");
        if(particles != null)
        {
            particles.collision.AddPlane(GameObject.Find("SPR_Floor").GetComponent<Transform>());
            particles.Play();
        }
    }
}
