using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float lifetime = 1f;
    public float minDist = 1f;
    public float maxDist = 2f;

    private Vector3 iniPos;
    private Vector3 targetPos;
    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        float direction = UnityEngine.Random.rotation.eulerAngles.z;
        iniPos = transform.position;
        float distX = UnityEngine.Random.Range(minDist, maxDist);
        float distY = UnityEngine.Random.Range(minDist, maxDist);
        targetPos = iniPos + (Quaternion.Euler(0, 0, direction) * new Vector3(distX, distY, 0));
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= lifetime) Destroy(gameObject);

        float fraction = lifetime / 2;
        if (timer > fraction)
        {
            text.color = Color.Lerp(text.color, Color.clear, (timer - fraction) / (lifetime - fraction));
        }

        transform.position = Vector3.Lerp(iniPos, targetPos, Mathf.Sin(timer/lifetime));

    }

    public void SetDamageText(float damage)
    {
        text.text = damage.ToString();
    }
}
