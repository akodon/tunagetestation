using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateGround : MonoBehaviour
{
    public GameObject[] Groundobj;
    int i = 0;

    public GameObject OnSwith;
    public GameObject OffSwith;
    private bool swithbool;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Groundobj.Length);
        OnSwith.SetActive(false);
        swithbool = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (i < Groundobj.Length)
        {
            i = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            swithbool = !swithbool;

            if (swithbool == true)
            {
                OnSwith.SetActive(true);
                OffSwith.SetActive(false);
            }

            if (swithbool == false)
            {
                OnSwith.SetActive(false);
                OffSwith.SetActive(true);
            }

            for ( i=0;i<=Groundobj.Length - 1 ; )
            {
                Vector3 localAngle = Groundobj[i].transform.localEulerAngles;
                localAngle.y = 90.0f;
                Groundobj[i].transform.localEulerAngles += localAngle;
                i++;

            }

           
        }
    }
}
