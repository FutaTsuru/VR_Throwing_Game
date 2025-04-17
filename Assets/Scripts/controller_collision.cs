using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Valve.VR;

public class controller_collision : MonoBehaviour
{
    [SerializeField] string controller_kind;

    pos pos_scripts;

    GameObject Player;

    void Start()
    {
        Player = GameObject.Find("Player");
        pos_scripts = Player.GetComponent<pos>();
    }

    // Start is called before the first frame update
    void OnCollisionEnter(Collision collision)
    {
        if (controller_kind == "left")
        {
            pos_scripts.vibration.Execute(0, 0.2f, 100, 1, SteamVR_Input_Sources.LeftHand);
            if (pos_scripts.b_left)
            {
                //çÌèúèàóù
                Destroy(collision.gameObject);
            }
        }

        if (controller_kind == "right")
        {
            pos_scripts.vibration.Execute(0, 0.2f, 100, 1, SteamVR_Input_Sources.RightHand);
            if (pos_scripts.b_right)
            {
                //çÌèúèàóù
                Destroy(collision.gameObject);
            }
        }

    }


    
}
