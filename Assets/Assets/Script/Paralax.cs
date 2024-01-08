using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    public float animation_speed = 1;

    private void Awake(){
        meshRenderer = GetComponent<MeshRenderer>();
    }
    private void Update(){
        meshRenderer.material.mainTextureOffset += new Vector2(animation_speed * Time.deltaTime, 0);
    }
}
