using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    [SerializeField]
    public string sceneName;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
<<<<<<< Updated upstream
        SceneManager.LoadScene("StevesLevelMilestone2");
=======
        SceneManager.LoadScene(sceneName);
>>>>>>> Stashed changes
    }
}

