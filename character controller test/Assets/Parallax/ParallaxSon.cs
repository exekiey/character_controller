using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxSon : MonoBehaviour
{

    bool _isVerticalParallax;

    public bool IsVerticalParallax {set => _isVerticalParallax = value; }

    // Start is called before the first frame update
    void Start()
    {

        SpriteRenderer spriteRenderer= GetComponent<SpriteRenderer>();

        Vector3 bounds = spriteRenderer.bounds.extents;


        float x = transform.position.x;
        float y = transform.position.y;
        float z = transform.position.z;


        float right = x + (bounds.x * 2);
        float left = x - (bounds.x * 2);

        float up = y + (bounds.y * 2);
        float down = y - (bounds.y * 2);

        Dictionary<String, Vector3> namePos = new Dictionary<String, Vector3>()
        {

            {"up", new Vector3(x, up, z) },
            {"upRight", new Vector3(right, up, z) },
            {"upLeft", new Vector3(left, up, z) },

            {"down", new Vector3(x, down, z) },
            {"downRight", new Vector3(right, down, z) },
            {"downLeft", new Vector3(left, down, z) },
            
            {"left", new Vector3(left, y, z) },
            {"right", new Vector3(right, y, z) },
            {"center", new Vector3(x, y, z) }

        };

        transform.position = namePos[gameObject.name];

    }
}
