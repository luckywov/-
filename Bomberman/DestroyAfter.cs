using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfter : MonoBehaviour
{
    public float time = 3f;
    private void Start()
    {
        //Ïú»ÙÕ¨µ¯
        Destroy(gameObject, time);
    }
}
