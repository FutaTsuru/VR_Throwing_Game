using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class text_management : MonoBehaviour
{
    [SerializeField] Text Result;

    [SerializeField] Text Score;

    [SerializeField] Text High_Score;

    // Start is called before the first frame update
    void Start()
    {
        Result.text = "倒した人数 : " + pos.stage + "人";

        Score.text = "今回の得点 : " + pos.total_score + "点";

        High_Score.text = "ハイスコア : " + pos.high_score + "点";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
