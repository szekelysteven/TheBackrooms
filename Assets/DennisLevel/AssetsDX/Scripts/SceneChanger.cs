using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{

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
}
