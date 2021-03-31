using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public UnityEngine.UI.Text logLabel;

    private string log;

    public void AddLog(string message)
    {
        log = log + "\n" + message;
        logLabel.text = log;
    }
}
