using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlashColor : MonoBehaviour
{
    public List<SpriteRenderer> spriteRenderers;
    public Color color = Color.red;
    public float duration = .1f;

    private void OnValidate()
    {
        spriteRenderers = new List<SpriteRenderer>();
        foreach (var child in transform.GetComponentsInChildren<SpriteRenderer>())
        {
            spriteRenderers.Add(child);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Flash();
        }
    }

    public void Flash()
    {
        foreach (var s in spriteRenderers)
        {
            s.DOColor(color, duration).SetLoops(2, LoopType.Yoyo).OnComplete(() =>
            {
                spriteRenderers.ForEach(i => i.color = Color.white);
            });
        }
    }
}
