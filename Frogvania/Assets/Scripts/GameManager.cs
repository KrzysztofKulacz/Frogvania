using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public int lives;
    public int points;

    public TextMeshProUGUI interfacePoints;
    public TextMeshProUGUI interfaceLives;


    private void Start()
    {
        Instance = this;
        lives = 3;
        points = 0;
    }

    public void AddPoint()
    {
        points++;
        interfacePoints.text = points.ToString();
    }

    public void SubLive()
    {
        lives--;
        interfaceLives.text = lives.ToString();

    }
}
