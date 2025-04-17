using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball_movement : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField] public bool start_flag;

    [SerializeField] public Animator throw_animation;

    [SerializeField] private AudioClip throw_sound;

    [SerializeField] private AudioClip collision_sound;

    [SerializeField] private float before_throw;

    [SerializeField] private float animation_time;

    [SerializeField] private GameObject bad_ball;

    [SerializeField] private GameObject heart;

    private bool throwed_flag = false;

    AudioSource effect_audio;

    pos pos_scripts;

    GameObject Player;

    //private int power = 900;

    //private Vector3 direction = new Vector3(-1, 0.48f, 0);

    //private Vector3 right_direction = new Vector3(-1, 0.45f, -0.1f);

    //private Vector3 left_direction = new Vector3(-1, 0.45f, 0.1f);

    private int power = 750;

    private Vector3 direction = new Vector3(-1, 0.58f, 0);

    private Vector3 right_direction = new Vector3(-1, 0.44f, -0.2f);

    private Vector3 left_direction = new Vector3(-1, 0.44f, 0.2f);

    private Vector3 standard_position;

    private Quaternion standard_rotaion;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");

        pos_scripts = Player.GetComponent<pos>();

        effect_audio = GetComponent<AudioSource>();

        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionY;

        standard_position = rb.position;

        standard_rotaion = rb.rotation;

        if (start_flag)
        {
            throwed_flag = true;
            throw_function();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < 0)
        {
            Debug.Log(transform.position);
        }

        if (start_flag && !throwed_flag)
        {
            throwed_flag = true;
            throw_function();
        }

        if (pos_scripts.Play_Mode == "Move" && throwed_flag)
        {
            Destroy(this.gameObject);
        }
    }

    private void throw_function()
    {
        //コースを選択
        int rnd = Random.Range(0, 3);

        //プレーヤーの正面に投げる
        if (rnd == 0)
        {
            StartCoroutine(throw_ball(direction));
        }

        //プレーヤーから見て右側に投げる
        else if (rnd == 1)
        {
            StartCoroutine(throw_ball(right_direction));
        }

        //プレーヤーから見て左側に投げる
        else if (rnd == 2)
        {
            StartCoroutine(throw_ball(left_direction));
        }
    }

    private IEnumerator throw_ball(Vector3 direction)
    {
        yield return new WaitForSeconds(before_throw - animation_time);
        throw_animation.SetBool("throw_motion_flag", true);
        yield return new WaitForSeconds(animation_time);
        throw_animation.SetBool("throw_motion_flag", false);
        rb.constraints = RigidbodyConstraints.None;
        rb.AddForce(direction * power);
        effect_audio.PlayOneShot(throw_sound);
        StartCoroutine(make_ball(0.25f));
    }

    private IEnumerator make_ball(float wait_minute)
    {
        yield return new WaitForSeconds(wait_minute);

        //ハートが出る確率は15％に設定する
        int rnd = Random.Range(0, 20);
        if (rnd <= 16)
        {
            GameObject new_ball = Instantiate(bad_ball, standard_position, standard_rotaion);
            new_ball.GetComponent<ball_movement>().throw_animation = throw_animation;
        }
        else
        {
            GameObject new_heart = Instantiate(heart, standard_position, Quaternion.Euler(new Vector3(-90, 0, 90)));
            new_heart.GetComponent<ball_movement>().throw_animation = throw_animation;
        }
    }
}
