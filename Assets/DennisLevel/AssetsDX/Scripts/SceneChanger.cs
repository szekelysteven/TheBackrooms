using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{

    public int buildIndexNumber;

    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
    public void LoadSubwayLevel()
    {
        SceneManager.LoadScene("StevesLevel");
    }
    public void LoadGandM()
    {
        SceneManager.LoadScene("Grace&Mercy");
    }
    public void LoadGasStation()
    {
        SceneManager.LoadScene("Albert_GasStation");
    }
    public void LoadHideoutLevel()
    {
        SceneManager.LoadScene("UndergroundLevel");
    }

    //Above is stuff for Buttons. Create an empty that can hold this script

    //Below is stuff for triggers


    public void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(buildIndexNumber);
    }
}
