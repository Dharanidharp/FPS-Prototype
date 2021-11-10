using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreText : MonoBehaviour
{
    public Text scoreText;
    static Text scoreTextIns;

    //public Text enemyText;
    //static Text enemyTextIns;

    private void Awake()
    {
        scoreTextIns = scoreText;
        //enemyTextIns = enemyText;
    }
    public static void setText(int score)
    {
        scoreTextIns.text = "Score: " + score;
    }

    public static void setEnemyText(int enemy)
    {
        //enemyTextIns.text = "Enemy: " + enemy;
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
