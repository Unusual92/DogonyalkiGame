using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{


    [Header("����� �����")]

    public int sceneNumber;

    public void Transition()
    {
        SceneManager.LoadScene(sceneNumber);
    }




        }