using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ROUProjection : MonoBehaviour
{
    public float timeTillDestroy;

    private void Start()
    {
        Destroy(this.gameObject, timeTillDestroy);
    }
}
