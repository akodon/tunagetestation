using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rootturnController : MonoBehaviour
{
    public GameObject turnRight;
    public GameObject turnLeft;

    public BoolRight boolright;
    public BoolLeft boolleft;


    // Start is called before the first frame update
    void Start()
    {
        turnRight =transform.Find("Right").gameObject ;
        turnLeft = transform.Find("Left").gameObject;

        boolright = turnRight.GetComponent<BoolRight>();
        boolleft = turnLeft.GetComponent<BoolLeft>();

        Debug.Log(turnLeft, turnRight);
    }


    // Update is called once per frame
    void Update()
    {
        if (boolleft.Lefton == true)
        {
            turnRight.SetActive(false);
        }

        if (boolright.Righton == true)
        {
            turnLeft.SetActive(false);
        }
    }
}
