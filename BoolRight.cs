using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoolRight : MonoBehaviour
{
    public bool Righton = false;

    // Start is called before the first frame update
    void Start()
    {
        Righton = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Righton = true;
        }
    }
}
