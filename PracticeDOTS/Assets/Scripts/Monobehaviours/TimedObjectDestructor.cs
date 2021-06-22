using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedObjectDestructor : MonoBehaviour
{
    [SerializeField] float delay;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyGameObject", delay);
    }

    // Update is called once per frame
    void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
