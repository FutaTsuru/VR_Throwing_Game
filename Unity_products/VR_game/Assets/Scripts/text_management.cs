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
        Result.text = "�|�����l�� : " + pos.stage + "�l";

        Score.text = "����̓��_ : " + pos.total_score + "�_";

        High_Score.text = "�n�C�X�R�A : " + pos.high_score + "�_";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
