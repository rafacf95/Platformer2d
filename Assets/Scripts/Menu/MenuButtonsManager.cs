using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class MenuButtonsManager : MonoBehaviour
{
    [Header("Animation Setup")]
    public float duration = .2f;
    public float delay = .05f;
    public List<GameObject> buttons;
    public Ease ease = Ease.OutBack;

    private void HideButtons()
    {
        foreach (var b in buttons)
        {
            b.transform.localScale = Vector3.zero;
            b.SetActive(false);
        }
    }
    private void ShowButtons()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            var b = buttons[i];
            Debug.Log(b);
            b.SetActive(true);
            b.transform.DOScale(1, duration).SetDelay(i * delay).SetEase(ease);
        }
    }

    // void OnEnable()
    // {
    //     HideButtons();
    //     ShowButtons();
    // }
    void Awake()
    {
        HideButtons();
        ShowButtons();
    }
}
