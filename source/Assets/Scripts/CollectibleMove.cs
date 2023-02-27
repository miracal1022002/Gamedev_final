using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleMove : MonoBehaviour
{
    public float amp = 0.6f;
    public float freq = 4f;  
    Vector3 initPos;

    private void Start() {
        initPos = transform.position;
    }

    private void Update() {
        transform.position = new Vector3(initPos.x, Mathf.Sin(Time.time * freq) * amp +initPos.y, 0);
    }
}
