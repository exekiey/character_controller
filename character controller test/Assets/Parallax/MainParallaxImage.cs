using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainParallaxImage : MonoBehaviour
{

    float _parallaxStrength;
    bool _isVerticalParallax;
    Sprite _sprite;
    Camera _cam;
    Vector3 _size;

    Vector3 _startPos;

    // Start is called before the first frame update
    public void Start()
    {
        SetSpritesToSons();

        _cam = Camera.main;

        _startPos = transform.position;
    }

    private void SetSpritesToSons()
    {
        foreach (Transform child in transform)
        {

            SpriteRenderer spriteRenderer = child.GetComponent<SpriteRenderer>();

            spriteRenderer.sprite = _sprite;

            _size = spriteRenderer.bounds.size;
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {

        Vector3 temp = (_cam.transform.position * (1 - _parallaxStrength));

        Vector3 dist = _cam.transform.position * _parallaxStrength;

        if (_isVerticalParallax )
        {

            transform.position = new Vector3(_startPos.x + dist.x, _startPos.y + dist.y, transform.position.z);
            
        } else
        {

            transform.position = new Vector3(_startPos.x + dist.x, _startPos.y, transform.position.z);

        }

        if (_isVerticalParallax )
        {

            FixY(temp);
        }

        FixX(temp);

    }

    private void FixY(Vector3 temp)
    {
        if (temp.y > _startPos.y + _size.y)
        {

            _startPos.y += _size.y;

        }
        else if (temp.y < _startPos.y - _size.y)
        {
            _startPos.y -= _size.y;
        }
    }

    private void FixX(Vector3 temp)
    {
        if (temp.x > _startPos.x + _size.x)
        {

            _startPos.x += _size.x;

        }
        else if (temp.x < _startPos.x - _size.x)
        {
            _startPos.x -= _size.x;
        }
    }

    public SpriteValuePair Values
    {
        set
        {
            _sprite = value.sprite;
            _parallaxStrength = value.value;
            _isVerticalParallax = value.isVerticalParallax;
            
        }
    }
}
