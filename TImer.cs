using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.UI;
public class Timer : MonoBehaviour
{
    public float startTime = 60;
    public Text timerLabel;
    // Start is catled before the first frame update
    void Start()
    {
        timerLabel.text = startTime.ToString();
    }

 //Update is called once per frame
        void Update()
    {
        startTime -= Time.deltaTime;
        timerLabel.text = Mathf.Round(startTime).ToString();

    }
}