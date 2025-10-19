using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Cinemachine;

public class CHeadDir : MonoBehaviour
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


    // Start is called before the first frame update
    void Start()
    {
        clickHitPos = player.transform.position;
        clickHitPos.y = -1.0f;
        raylineOn = false;

        straighttext.text = "のこり："+ straightcount;
        crosstext.text = "のこり：" + crosscount;

        audiosource = GetComponent<AudioSource>();



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


    }

    //ボタンSE制御
    public void OnStraight()
    {
        StraightSet = !StraightSet;


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
