using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildProcessBar : MonoBehaviour
{
    public GameObject process;
    // Start is called before the first frame update
    void Start()
    {
        setValue(0.01f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setValue(float per)
    {
        process.GetComponent<RectTransform>().sizeDelta = new Vector2(3f * per, 0.5f);
    }
}
