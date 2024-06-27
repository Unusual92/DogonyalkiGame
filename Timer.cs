using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timer;

    public float lifeTime = 60f;
    private float gameTime;
    public int nextSceneName; 



    void Update()
    {
        timer.text = lifeTime + " сек";
        gameTime += 1 * Time.deltaTime;
        if (gameTime >= 1)
        {
            lifeTime -= 1;
            gameTime = 0;
        }
        if (lifeTime <= 10)
        {
            timer.color = Color.yellow;
        }
        if (lifeTime <= 3)
        {
            timer.color = Color.red;
        }

        if (lifeTime <= 0) 
        {
            SceneManager.LoadScene(nextSceneName); //переход на следующую сцену
        }
    }


}
