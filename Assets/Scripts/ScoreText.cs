using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreText : MonoBehaviour
{
    public Text scoreText;
    static Text scoreTextIns;

    private void Awake()
    {
        scoreTextIns = scoreText;
    }
    public static void setText(int score)
    {
        scoreTextIns.text = "Score: " + score;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
