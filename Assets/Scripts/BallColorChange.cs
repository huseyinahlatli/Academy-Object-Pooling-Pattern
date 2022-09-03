using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallColorChange : MonoBehaviour
{
    private void Start()
    {
        SetRandomColor(gameObject);
    }

    private void SetRandomColor(GameObject newObject)
    {
        Color newColor = new Color(
            Random.Range(0f, 1f),
            Random.Range(0f, 1f),
            Random.Range(0f, 1f)
        ); 
        newObject.GetComponent<Renderer>().material.color = newColor;
    }
    
}
