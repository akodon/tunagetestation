using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Cinemachine;

public class CHeadDir_tutorial : MonoBehaviour
{
    public GameObject player;
    private Vector3 forceDir;

    Ray ray;
    RaycastHit hit;
    public Camera cam;
    Vector3 clickHitPos;
    public bool raylineOn;

    public GameObject straight;
    public GameObject cross;
    private GameObject instanceobj;

    public bool StraightSet = false;
    public bool CrossSet = false;
    public bool Deletebool = false;

    public int  straightcount;
    public int crosscount;

    public Button straightbutton;       //直線線路ボタン
    public Button crossbutton;          //曲線線路ボタン
    public Button Dalatebutton;         //取り消しボタン
    public TextMeshProUGUI straighttext;
    public TextMeshProUGUI crosstext;

    //音関係
    private AudioSource audiosource;
    public AudioClip SetSE;
    public AudioClip RotateSE;
    public AudioClip ButtonSE;
    public AudioClip ButtonCanselSE;

    //チュートリアル判定
    public bool[] tutorialstep;
    public GameObject[] tutorialtext;

    // Start is called before the first frame update
    void Start()
    {
        clickHitPos = player.transform.position;
        clickHitPos.y = -1.0f;
        raylineOn = false;

        straighttext.text = "のこり："+ straightcount;
        crosstext.text = "のこり：" + crosscount;

        audiosource = GetComponent<AudioSource>();

        for (int i = 0; i >= tutorialstep.Length; i++)
        {
            tutorialstep[i] = false;
            tutorialtext[i].SetActive(false);
        }

        tutorialstep[0] = true;
        

    }

    // Update is called once per frame
    void Update()
    {
        //例を飛ばす
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Debug.DrawRay(cam.transform.position, ray.direction * 50f, Color.red);
            Vector3 hitPos = hit.point;

            if (Input.GetMouseButton(0))
            {
                raylineOn = true;
                clickHitPos = hitPos;
                clickHitPos.y = -1.0f;

            }
            if (hit.collider.tag == "Ground")
            {
                if (Input.GetMouseButtonDown(0))
                 {
                    tutorialstep[1] = false;
                    tutorialstep[2] = true;

                    if (StraightSet == true && straightcount > 0)
                     {
                        audiosource.PlayOneShot(SetSE);
                        instanceobj = Instantiate(straight, hit.collider.gameObject.transform.position, Quaternion.identity);
                        instanceobj.transform.parent = hit.collider.gameObject.transform;
                        straightcount -= 1;
                        straighttext.text = "のこり：" + straightcount;
                    }
                     if (CrossSet == true && crosscount > 0)
                     {
                        audiosource.PlayOneShot(SetSE);
                        instanceobj = Instantiate(cross, hit.collider.gameObject.transform.position, Quaternion.identity);
                        instanceobj.transform.parent = hit.collider.gameObject.transform;
                        crosscount -= 1;
                        crosstext.text = "のこり：" + crosscount;
                    }

                }

             }

             if (hit.collider.tag == "straight" || hit.collider.tag == "Cross")
             {
                if (Input.GetMouseButtonDown(1))
                {
                    audiosource.PlayOneShot(RotateSE);

                    Transform turntransform = hit.collider.gameObject.transform;
                    Vector3 localAngle = turntransform.localEulerAngles;
                    localAngle.y = 90.0f;
                    turntransform.localEulerAngles += localAngle;
                    tutorialstep[2] = false;
                    tutorialstep[3] = true;
                }
             }

             if(Deletebool == true)
            {
                if (hit.collider.tag == "straight")
                {
                    if (Input.GetMouseButton(0))
                    {
                        Destroy(hit.collider.gameObject);
                        straightcount += 1;
                        straighttext.text = "のこり：" + straightcount;
                    }
                }

                if (hit.collider.tag == "Cross")
                {
                    if (Input.GetMouseButton(0))
                    {
                        Destroy(hit.collider.gameObject);
                        crosscount += 1;
                        crosstext.text = "のこり：" + crosscount;
                    }
                }
            }

            if (crosscount <= 0 && straightcount <= 0 && tutorialstep[3] == true&&tutorialstep[2]==false)
            {
                tutorialtext[0].SetActive(false);
                tutorialtext[1].SetActive(false);
                tutorialtext[2].SetActive(false);
                tutorialtext[3].SetActive(true);
            }



     
        }


        if (StraightSet == true)
        {
            straightbutton.GetComponent<Image>().color = Color.gray;
        }

        else if (StraightSet == false)
        {
            straightbutton.GetComponent<Image>().color = Color.white;
        }

        if (CrossSet == true)
        {
            crossbutton.GetComponent<Image>().color = Color.gray;
        }

        else if (CrossSet == false)
        {
            crossbutton.GetComponent<Image>().color = Color.white;
        }

        if (Deletebool == true)
        {
            Dalatebutton.GetComponent<Image>().color = new Color(0.42f, 0.7f, 0.7f);
        }

        else if (Deletebool == false)
        {
            Dalatebutton.GetComponent<Image>().color = new Color(0.5215687f, 0.8039216f, 0.8588236f);
        }


        //tutorial制御
        if (tutorialstep[0] == true)
        {
            tutorialtext[0].SetActive(true);
            tutorialtext[1].SetActive(false);
            tutorialtext[2].SetActive(false);
            tutorialtext[3].SetActive(false);
        }

        if (tutorialstep[0] == false)
        {
            tutorialtext[0].SetActive(false);
        }

        if (tutorialstep[1] == true)
        {
            tutorialtext[1].SetActive(true);
            tutorialtext[0].SetActive(false);
            tutorialtext[2].SetActive(false);
            tutorialtext[3].SetActive(false);
        }
        if (tutorialstep[1] == false)
        {
            tutorialtext[1].SetActive(false);

        }
        if (tutorialstep[2] == true)
        {
            tutorialtext[2].SetActive(true);
            tutorialtext[0].SetActive(false);
            tutorialtext[1].SetActive(false);
            tutorialtext[3].SetActive(false);
        }

        if (tutorialstep[2] == false)
        {
            tutorialtext[2].SetActive(false);

        }

        if (tutorialstep[3] == false)
        {
            tutorialtext[3].SetActive(false);
        }


    }

    //ボタンSE制御
    public void OnStraight()
    {
        StraightSet = !StraightSet;

        tutorialstep[0] = false;
        tutorialstep[1] = true;
        Debug.Log("tutorialです");

        if (StraightSet == true)
        {
            audiosource.PlayOneShot(ButtonSE);
        }

        else if (StraightSet == false)
        {
            audiosource.PlayOneShot(ButtonCanselSE);
        }

        if (CrossSet == true || Deletebool == true)
        {
            CrossSet = false;
            Deletebool = false;
        }
    }

    public void OnCross()
    {
        CrossSet = !CrossSet;
        tutorialstep[0] = false;
        tutorialstep[1] = true;

        if (CrossSet == true)
        {
            audiosource.PlayOneShot(ButtonSE);
        }

        else if (CrossSet == false)
        {
            audiosource.PlayOneShot(ButtonCanselSE);
        }

        if (StraightSet == true||Deletebool == true)
        {
            StraightSet = false;
            Deletebool = false;
        }
    }

    public void DestroyObject()
    {
        Deletebool =! Deletebool;

        if (Deletebool == true)
        {
            audiosource.PlayOneShot(ButtonSE);
        }

        else if (Deletebool == false)
        {
            audiosource.PlayOneShot(ButtonCanselSE);
        }

        if (StraightSet == true || CrossSet == true)
        {
            StraightSet = false;
            CrossSet = false;
        }
    }

}
