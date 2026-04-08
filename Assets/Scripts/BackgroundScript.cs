using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public Transform camTransform;
    
    // This controls how "big" the grid squares look in-game
    public float gridScale = 0.1f; 

    void LateUpdate()
    {
        if (camTransform == null) return;

        // 1. Keep the Quad centered on the camera
        transform.position = new Vector3(camTransform.position.x, camTransform.position.y, 10f);

        // 2. Calculate the offset
        // We divide by the object's scale to keep the texture aligned with world units
        Vector2 offset = new Vector2(camTransform.position.x, camTransform.position.y) * gridScale;

        // 3. Apply the offset to the material
        meshRenderer.material.mainTextureOffset = offset;
    }
}
