using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoolLeft : MonoBehaviour
{
    public bool Lefton = false;

    // Start is called before the first frame update
    void Start()
    {
        Lefton = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Lefton = true;
        }
    }
}
