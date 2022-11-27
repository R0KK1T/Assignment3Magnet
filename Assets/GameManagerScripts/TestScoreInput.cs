using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScoreInput : MonoBehaviour
{
    public GameObject gameManager;
    private bool spacePressed = false;
    private bool mPressed = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space") && !spacePressed)
        {
            gameManager.GetComponent<GameManager>().ScoreChange(1);
            spacePressed = true;
        }
        else if (Input.GetKeyUp("space") && spacePressed)
        {
            spacePressed = false;
        }


        if (Input.GetKeyDown("m") && !mPressed)
        {
            gameManager.GetComponent<GameManager>().ScoreChange(-1);
            mPressed = true;
        }
        else if (Input.GetKeyUp("m") && mPressed)
        {
            mPressed = false;
        }
    }
}
