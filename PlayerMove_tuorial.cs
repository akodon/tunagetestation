using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;

public class PlayerMove_tuorial : MonoBehaviour
{

    public bool startbool = false;
    Rigidbody rigidbody;
    private float movetransform;

    public GameObject GameCnt;

    public CHeadDir_tutorial cHeadDir_Tutorial;

    public GameObject textobj;
    public GameObject GameClearobj;
    public GameObject GamePlayUI;

    public AudioSource audiosourceA;
    public AudioSource audiosourceB;

    private AudioSource audiosource;
    public AudioClip FinishSE;

    public CinemachineVirtualCamera finishcam;


    // Start is called before the first frame update
    void Start()
    {
        startbool = false;
        rigidbody = this.GetComponent<Rigidbody>();
        rigidbody.useGravity = false;

        cHeadDir_Tutorial = GameCnt.GetComponent<CHeadDir_tutorial>();
        textobj.SetActive(false);
        GameClearobj.SetActive(false);

        audiosourceA.Play();
        audiosourceB.Stop();

        audiosource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (movetransform >= 0.0f && movetransform <= 89.9f)
        {
            movetransform = 90.0f;
        }

        else if (movetransform >= 90.0f &&  movetransform <= 179.9f)
        {
            movetransform = 180.0f;
        }

        else if (movetransform >= 180.0f && movetransform <= 269.9f)
        {
            movetransform = 270.0f;
        }

        else if (movetransform >= 270.0f && movetransform <= 359.9f)
        {
            movetransform = 0.0f;
        }

        if(movetransform <= -270.0f && movetransform >= -359.9f)
        {
            movetransform = 0.0f;
        }

        else if (movetransform <= -180.0f && movetransform >= -269.9f)
        {
            movetransform = 90.0f;
        }

        else if (movetransform <= -90.0f && movetransform >= -179.9f)
        {
            movetransform = 180.0f;
        }

        else if (movetransform <= 0.0f && movetransform >= -89.9f)
        {
            movetransform = 270.0f;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Finish")
        {
            finishcam.Priority += 10;
            startbool = false;
            audiosourceB.Stop();
            GamePlayUI.SetActive(false);
            GameClearobj.SetActive(true);
            audiosource.PlayOneShot(FinishSE);

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (startbool == true)
        {

            if (other.gameObject.tag == "straight")
            {
                movetransform = other.gameObject.transform.localEulerAngles.y - this.gameObject.transform.localEulerAngles.y;
                movetransform = Mathf.Floor(movetransform);

                Debug.Log(movetransform);

                if (movetransform == 0.0f || movetransform == 180.0f || movetransform==-180.0f)
                {
                    transform.Translate(0.05f, 0, 0);
                }
            }

            if (other.gameObject.tag == "Cross")
            {
                movetransform = other.gameObject.transform.localEulerAngles.y - this.gameObject.transform.localEulerAngles.y;
                movetransform = Mathf.Floor(movetransform);

                transform.Translate(0.05f, 0, 0);


            }

        }

    }

    private void OnTriggerExit(Collider other)
    {

        movetransform = other.gameObject.transform.localEulerAngles.y - this.gameObject.transform.localEulerAngles.y;
        movetransform = Mathf.Floor(movetransform);

            if (other.gameObject.tag == "Right")
            {
                transform.Rotate(0, 90, 0);
            }

            else if (other.gameObject.tag == "Left")
            {
                transform.Rotate(0, -90, 0);

            }


      
    }

    public void OnStart()
    {

        if (cHeadDir_Tutorial.straightcount == 0 && cHeadDir_Tutorial.crosscount == 0)
        {
            audiosourceA.Stop();
            audiosourceB.Play();

            startbool = true;
            rigidbody.useGravity = true;

            cHeadDir_Tutorial.tutorialstep[3] = false;
        }

        else
        {
            textobj.SetActive(true);
            Invoke("falseObjects", 3.0f);
        }

    }

    private void falseObjects()
    {
        textobj.SetActive(false);
    }

}
