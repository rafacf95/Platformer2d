using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening.Core.Easing;
using UnityEngine;

public class HealthBase : MonoBehaviour
{
    [Header("Health Setup")]
    public int startLife = 10;
    public bool destroyOnKill = false;
    public float destroyDelay = 1f;
    public Action OnKill;

    [SerializeField] private int _currentLife;
    [SerializeField] private bool _isDead = false;
    [SerializeField] private FlashColor _flashColor;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _isDead = false;
        _currentLife = startLife;

        if (_flashColor == null)
        {
            _flashColor = gameObject.GetComponent<FlashColor>();
        }
    }

    public void Damage(int damage)
    {
        if (_isDead) return;

        _currentLife -= damage;

        if (_currentLife <= 0)
        {
            Kill();
        }

        if (_flashColor != null)
        {
            _flashColor.Flash();
        }
    }

    private void Kill()
    {
        _isDead = true;

        if (destroyOnKill)
        {
            Destroy(gameObject, destroyDelay);
        }

        OnKill?.Invoke();
    }
}
