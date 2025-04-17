using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cube_move : MonoBehaviour
{
    [SerializeField] string cube_kind;

    pos pos_scripts;

    GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        pos_scripts = Player.GetComponent<pos>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cube_kind == "left")
        {
            //transform.position = pos_scripts.LeftHandPosition;
        }
        
        else if (cube_kind == "right")
        {
            //transform.position = pos_scripts.RightHandPosition;
        }
    }
}
