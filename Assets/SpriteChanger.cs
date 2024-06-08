using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteChanger : MonoBehaviour
{
    [SerializeField] private Image _target;
    [SerializeField] private Sprite _gragSprites;
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private float _delay;

    private WaitForSeconds _sleep;
    private int _currentIndex = 0;

    private Coroutine _coroutine;

    void Start()
    {
        _sleep = new WaitForSeconds(_delay);

        SetMoveState();
    }

    public void SetMoveState() 
    {
        StopMove();
        _coroutine = StartCoroutine(Animation());
    }

    public void SetDragState() 
    {
        StopMove();
        _target.sprite = _gragSprites;
    }

    private void StopMove()
    {
        if (_coroutine != null) 
        {
            StopCoroutine(_coroutine);
        }
        _coroutine = null;
    }

    private IEnumerator Animation() 
    {
        while (_sprites != null)
        {
            yield return _sleep;
            _target.sprite = _sprites[_currentIndex];

            _currentIndex++;

            if (_currentIndex >= _sprites.Length) 
            {
                _currentIndex = 0;
            }
        }
    }
}
