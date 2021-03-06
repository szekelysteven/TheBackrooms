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
        SceneManager.LoadScene("WalderhaugAlbert_GasStation");
    }
    public void LoadHideoutLevel()
    {
        SceneManager.LoadScene("UndergroundLevel");
    }
    public void LoadChurchLevel()
    {
        SceneManager.LoadScene("Walderhaug Albert_Church");
    }
    public void LoadGraveyardLevel()
    {
        SceneManager.LoadScene("Graveyard");
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void SceneSelector()
    {
        SceneManager.LoadScene("DebugScene");
    }

    //Above is stuff for Buttons. Create an empty that can hold this script

    //Below is stuff for triggers


    public void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(buildIndexNumber);
    }
    public void QuitButton()
    {
        Application.Quit();
    }
}
