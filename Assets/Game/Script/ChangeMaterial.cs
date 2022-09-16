using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterial : MonoBehaviour
{
    public Material[] material;
    public Renderer rend;
    void Start()
    {
        rend.enabled = true;
        rend.sharedMaterial = material[0];
    }
    public void MakeTransparent()
    {
        rend.sharedMaterial = material[1];
    }
    public void GiveColor()
    {
        rend.sharedMaterial = material[0];
    }
}
