using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation2D : MonoBehaviour
{
    public string Sprite;
    public float RunSpeed = 1;

    public bool Loop = true;

    private Sprite[] _sprite;
    private int _currentSprite;
    private float _spriteStep;

    // Start is called before the first frame update
    void Start()
    {
        _sprite = Resources.LoadAll<Sprite>("Sprites/" + Sprite);
        _currentSprite = 0;
        _spriteStep = 0.0f;

        if (_sprite.Length == 0)
        {
            Debug.Log("Die Animation " + Sprite + " wurde nicht gefunden! Erwartete Position: /Resources/Sprites/" + Sprite);
        }
    }

    public Sprite GetNext()
    {
        if (_sprite.Length == 0) return null;

        _spriteStep += Time.deltaTime;
        if (_spriteStep > 1 / RunSpeed)
        {
            _spriteStep = 0;

            if (++_currentSprite >= _sprite.Length)
            {
                if (Loop)
                {
                    _currentSprite = 0;
                }
                else
                {
                    --_currentSprite;
                }
            }
        }
        return _sprite[_currentSprite];
    }

    public bool IsFinished()
    {
        return !Loop && _currentSprite >= _sprite.Length - 1;
    }
}
