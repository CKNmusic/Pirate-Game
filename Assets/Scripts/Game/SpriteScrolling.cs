using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteScrolling : MonoBehaviour
{
    public Vector2 ScrollSpeed;

    private void OnEnable()
    {
        GetComponent<SpriteRenderer>().material.SetVector("_ScrollSpeed", ScrollSpeed);
    }
}

