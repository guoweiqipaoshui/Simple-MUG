using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointCounter : MonoBehaviour
{
    public static int Perfect = 0;
    public static int Miss = 0;
    public Text PerfectC, MissC;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
        PerfectC.text = Perfect.ToString();
        MissC.text = Miss.ToString();
    }


    public void Counter()
    {

    }
}
