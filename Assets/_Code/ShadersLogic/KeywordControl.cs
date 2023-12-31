﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeywordControl : MonoBehaviour
{
    private Material material;

    private void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        bool toggle = Time.time % 2.0f > 1.0f;

        if(toggle)
        {
            // Debug.Log("red");
            material.EnableKeyword("OVERRIDE_RED_ON");
        }
        else
        {
            // Debug.Log("redoff");
            material.DisableKeyword("OVERRIDE_RED_ON");
        }
    }
}