using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation2D : MonoBehaviour
{
    public string Sprite;
    public float RunSpeed = 1;

    private Sprite[] _sprite;
    private int _currentSprite;
    private float _spriteStep;

    // Start is called before the first frame update
    void Start()
    {
        _sprite = Resources.LoadAll<Sprite>("Sprites/" + Sprite);
        _currentSprite = 0;
        _spriteStep = 0.0f;
    }

    public Sprite GetNext()
    {
        _spriteStep += Time.deltaTime;
        if (_spriteStep > 1 / RunSpeed)
        {
            _spriteStep = 0;

            if (++_currentSprite >= _sprite.Length)
            {
                _currentSprite = 0;
            }
        }
        return _sprite[_currentSprite];
    }
}
