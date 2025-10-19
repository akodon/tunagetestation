using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("titleScene");
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void Stage1()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Stage2()
    {
        SceneManager.LoadScene("GameDesign12_02");
    }

    public void Stage3()
    {
        SceneManager.LoadScene("GameDesign12_03");
    }

    public void Stage4()
    {
        SceneManager.LoadScene("GameDesign12_04");
    }

    public void title()
    {
        SceneManager.LoadScene("titleScene");
    }
}
