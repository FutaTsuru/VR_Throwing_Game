using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class target_change : MonoBehaviour
{
    [SerializeField] GameObject target_30;

    [SerializeField] GameObject target_50;

    [SerializeField] GameObject target_100;

    [SerializeField] GameObject target_150;

    [SerializeField] GameObject target_minus;

    private GameObject[] GameObjects_list;

    [SerializeField] Vector3 first_position;

    [SerializeField] Vector3 second_position;

    [SerializeField] Vector3 third_position;

    [SerializeField] Vector3 forth_position;

    [SerializeField] AudioClip pon_sound;

    AudioSource effect_audio;

    private Vector3[] position_list;

    private float timeElapsed;

    private float change_time = 10;

    public string Play_Mode = "Play";


    // Start is called before the first frame update
    void Start()
    {
        effect_audio = GetComponent<AudioSource>();

        GameObjects_list = new GameObject[5] { target_30, target_50, target_100, target_150, target_minus };

        position_list = new Vector3[4] {first_position, second_position, third_position, forth_position};

        make_target();
    }

    // Update is called once per frame
    void Update()
    {
        if (Play_Mode == "Play")
        {
            timeElapsed += Time.deltaTime;

            if (timeElapsed > change_time)
            {
                timeElapsed = 0;

                make_target();
            }
        }

        else
        {
            timeElapsed = 0;
        }

    }

    public void make_target()
    {
        for (int i = 0; i < position_list.Length; i++)
        {
            int rnd = Random.Range(0, 5);

            Instantiate(GameObjects_list[rnd], position_list[i], Quaternion.Euler(new Vector3(90, 0, 90)));
        }

        effect_audio.PlayOneShot(pon_sound);
    }

    public void update_position()
    {
        first_position.x += 20;
        second_position.x += 20;
        third_position.x += 20;
        forth_position.x += 20;
        position_list = new Vector3[4] { first_position, second_position, third_position, forth_position };
    }
}
