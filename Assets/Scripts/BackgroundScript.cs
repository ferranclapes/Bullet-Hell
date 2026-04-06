using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public float scrollSensitivity = 0.1f;
    private Transform camTransform;

    void Start()
    {
        camTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        float offsetX = camTransform.position.x * scrollSensitivity;
        float offsetY = camTransform.position.y * scrollSensitivity;
        
        meshRenderer.material.mainTextureOffset = new Vector2(offsetX, offsetY);
    }
}
