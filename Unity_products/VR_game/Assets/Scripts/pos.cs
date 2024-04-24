using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using Valve.VR;
using System.Collections;
using Valve.VR.InteractionSystem;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SceneManagement;


public class pos : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField] GameObject LeftHand;

    [SerializeField] GameObject RightHand;

    target_change target_scripts;

    [SerializeField] GameObject Change_Target;

    [SerializeField] GameObject HealthBarHUDTester;

    [SerializeField] GameObject Damage;

    [SerializeField] GameObject Timer;

    Timer timer_script;

    [SerializeField] private AudioClip collision_sound;

    [SerializeField] private AudioClip grip_sound;

    [SerializeField] private AudioClip recover_sound;

    [SerializeField] private AudioClip run_sound;

    [SerializeField] Text ScoreUI;

    public Boolean left_throw_mode = false;

    public Boolean right_throw_mode = false;

    private Boolean walk_flag = false;

    AudioSource effect_audio;

    HealthBarHUDTester heart_operation;

    public string Play_Mode = "Play";

    public int Life_num = 3;

    private float stage_time = 30;

    public static int stage = 0;

    private string[] Spike_ball_list = new string[5] { "First_SpikeBall", "Second_SpikeBall", "Third_SpikeBall", "Forth_SpikeBall", "Fifth_SpikeBall" };

    public static int total_score;

    public static int high_score;

    private float y_rotation;

    private float y_radiun;

    private Vector3 current_position;

    private Vector3 current_velocity;

    private float pai = 3.14f;

    //HMDの位置座標格納用
    private Vector3 HMDPosition;
    //HMDの回転座標格納用（クォータニオン）
    private Quaternion HMDRotationQ;
    //HMDの回転座標格納用（オイラー角）
    private Vector3 HMDRotation;

    //左コントローラの位置座標格納用
    public Vector3 LeftHandPosition;
    //左コントローラの回転座標格納用（クォータニオン）
    public Quaternion LeftHandRotationQ;
    //左コントローラの回転座標格納用
    public Vector3 LeftHandRotation;

    //右コントローラの位置座標格納用
    public Vector3 RightHandPosition;
    //右コントローラの回転座標格納用（クォータニオン）
    public Quaternion RightHandRotationQ;
    //右コントローラの回転座標格納用
    public Vector3 RightHandRotation;

    public Vector3 LeftHandVelocity;

    public Vector3 RightHandVelocity;   

    //InteractUIボタンが押されてるのかを判定するためのIuiという関数にSteamVR_Actions.default_InteractUIを固定
    private SteamVR_Action_Boolean Iui = SteamVR_Actions.default_InteractUI;

    private SteamVR_Action_Boolean A_button = SteamVR_Actions.default_click_A;

    private SteamVR_Action_Boolean B_button = SteamVR_Actions.default_click_B;

    private SteamVR_Action_Boolean north_stick = SteamVR_Actions.default_Teleport;

    private SteamVR_Action_Boolean east_stick = SteamVR_Actions.default_SnapTurnRight;

    private SteamVR_Action_Boolean west_stick = SteamVR_Actions.default_SnapTurnLeft;

    private SteamVR_Action_Boolean south_stick = SteamVR_Actions.default_snapturnback;

    private Boolean interacrtu_left;

    private Boolean interacrtu_right;

    private Boolean a_left;

    public Boolean b_left;

    private Boolean a_right;

    public Boolean b_right;

    private Boolean north_left;

    private Boolean north_right;

    private Boolean east_left;

    private Boolean east_right;

    private Boolean west_left;

    private Boolean west_right;

    private Boolean south_left;

    private Boolean south_right;

    //振動の入力のためにvibrationという関数にSteamVR_Actions.default_Hapticを固定
    public SteamVR_Action_Vibration vibration = SteamVR_Actions.default_Haptic;

    private string head_position = "center";

    public Boolean make_cube_flag = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        effect_audio = GetComponent<AudioSource>();
        target_scripts = Change_Target.GetComponent<target_change>();
        Play_Mode = "Play";
        total_score = 0;
        heart_operation = HealthBarHUDTester.GetComponent<HealthBarHUDTester>();
        timer_script = Timer.GetComponent<Timer>();
        high_score = PlayerPrefs.GetInt("SCORE", 0);
    }

    //1フレーム毎に呼び出されるUpdateメゾット
    void Update()
    {
        /*InputTracking.GetLocalPosition(XRNode.機器名)で機器の位置や向きを呼び出せる*/

        //Head（ヘッドマウンドディスプレイ）の情報を一時保管-----------
        //位置座標を取得
        HMDPosition = InputTracking.GetLocalPosition(XRNode.Head);
        //回転座標をクォータニオンで値を受け取る
        HMDRotationQ = InputTracking.GetLocalRotation(XRNode.Head);
        //取得した値をクォータニオン → オイラー角に変換
        HMDRotation = HMDRotationQ.eulerAngles;
        //--------------------------------------------------------------

        //LeftHand（左コントローラ）の情報を一時保管--------------------
        //速度取得
        LeftHandVelocity = (InputTracking.GetLocalPosition(XRNode.LeftHand) - LeftHandPosition) / Time.deltaTime;
        //位置座標を取得
        LeftHandPosition = LeftHandPosition + LeftHandVelocity * Time.deltaTime;
        //回転座標をクォータニオンで値を受け取る
        LeftHandRotationQ = InputTracking.GetLocalRotation(XRNode.LeftHand);
        //取得した値をクォータニオン → オイラー角に変換
        LeftHandRotation = LeftHandRotationQ.eulerAngles;
        //--------------------------------------------------------------


        //RightHand（右コントローラ）の情報を一時保管--------------------
        //速度取得
        RightHandVelocity = (InputTracking.GetLocalPosition(XRNode.RightHand) - RightHandPosition) / Time.deltaTime;
        //位置座標を取得
        RightHandPosition = RightHandPosition + RightHandVelocity * Time.deltaTime;
        //回転座標をクォータニオンで値を受け取る
        RightHandRotationQ = InputTracking.GetLocalRotation(XRNode.RightHand);
        //取得した値をクォータニオン → オイラー角に変換
        RightHandRotation = RightHandRotationQ.eulerAngles;
        //--------------------------------------------------------------


        interacrtu_left = Iui.GetState(SteamVR_Input_Sources.LeftHand);

        interacrtu_right = Iui.GetState(SteamVR_Input_Sources.RightHand);

        a_left = A_button.GetState(SteamVR_Input_Sources.LeftHand);

        b_left = B_button.GetState(SteamVR_Input_Sources.LeftHand);

        a_right = A_button.GetState(SteamVR_Input_Sources.RightHand);

        b_right = B_button.GetState(SteamVR_Input_Sources.RightHand);

        north_left = north_stick.GetState(SteamVR_Input_Sources.LeftHand);

        north_right = north_stick.GetState(SteamVR_Input_Sources.RightHand);

        east_left = east_stick.GetState(SteamVR_Input_Sources.LeftHand);

        east_right = east_stick.GetState(SteamVR_Input_Sources.RightHand);

        west_left = west_stick.GetState(SteamVR_Input_Sources.LeftHand);

        west_right = west_stick.GetState(SteamVR_Input_Sources.RightHand);

        south_left = south_stick.GetState(SteamVR_Input_Sources.LeftHand);

        south_right = south_stick.GetState(SteamVR_Input_Sources.RightHand);

        current_velocity = (transform.position - current_position) / Time.deltaTime;

        current_position = current_velocity * Time.deltaTime + current_position;

        ScoreUI.text = "Score : " + total_score + "点";

        //Moveモード時の処理
        if (Play_Mode == "Move")
        {
            rb.rotation = Quaternion.Euler(new Vector3(0, 90, 0));

            y_rotation = transform.localRotation.eulerAngles.y % 360;

            y_radiun = (y_rotation / 360) * 2 * pai;

            //次のステージに到着した際の処理
            if (transform.position.x >= 20 * stage)
            {
                transform.position = new Vector3(20 * stage, 10, 0);
                Play_Mode = "Play";
                GameObject spike_ball = GameObject.Find(Spike_ball_list[stage]);
                ball_movement ball_script = spike_ball.GetComponent<ball_movement>();
                ball_script.start_flag = true;
                effect_audio.Stop();
                target_scripts.Play_Mode = "Play";
                target_scripts.make_target();
                timer_script.StartTimer();
            }

            //スティック処理
            stick_move(y_radiun);

            //走る効果音の処理
            if (current_velocity.magnitude > 0 && !walk_flag)
            {
                Debug.Log("run sound");

                walk_flag = true;

                effect_audio.PlayOneShot(run_sound);
            }

            else if (current_velocity.magnitude == 0 && walk_flag) 
            {
                effect_audio.Stop();
                walk_flag = false;
            }

        }

        //Playモード時の処理
        if (Play_Mode == "Play")
        {
            transform.position = new Vector3(stage * 20, 10, 0);

            stage_time -= Time.deltaTime;

            //残り時間が０になったら、移動モードに切り替え.
            if (stage_time < 0)
            {
                Play_Mode = "Move";
                stage_time = 30;
                stage += 1;

                if (stage == 5)
                {
                    StartCoroutine(GameOver(2));
                }

                head_position = "center";
                target_scripts.Play_Mode = "Move";
                target_scripts.update_position();
            }


            //HMDによる頭の位置の切り替え処理
            if (Mathf.Abs(HMDRotationQ.z) < 0.1 && head_position != "center")
            {
                head_position = "center";
                rb.position = new Vector3(0, 10, 0);
                rb.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
            }

            else if (HMDRotationQ.z > 0.2 && head_position != "right")
            {
                rb.rotation = Quaternion.Euler(new Vector3(0, 90, -40));
                //rb.constraints = RigidbodyConstraints.None;
                //rb.constraints = RigidbodyConstraints.FreezeRotation;
                //rb.constraints = RigidbodyConstraints.FreezePosition;
                head_position = "right";
                
            }

            else if (HMDRotationQ.z < -0.2 && head_position != "left")
            {
                head_position = "left";
                rb.rotation = Quaternion.Euler(new Vector3(0, 90, 40));
                rb.position = new Vector3(0, 10, 0);
            }


            //トリガーボタンを押したら、投球可能状態に遷移
            if (interacrtu_left && !left_throw_mode)
            {
                Debug.Log("left trigger");
                effect_audio.PlayOneShot(grip_sound);
                left_throw_mode = true;
            }

            if (interacrtu_right && !right_throw_mode)
            {
                Debug.Log("right trigger");
                effect_audio.PlayOneShot(grip_sound);
                right_throw_mode = true;
            }

            //投球可能状態で、トリガーボタンを押したら投球する.
            if (!interacrtu_left && left_throw_mode)
            {
                Debug.Log("left throw !!");
                left_throw_mode = false;
            }

            if (!interacrtu_right && right_throw_mode)
            {
                Debug.Log("right throw !!");
                right_throw_mode = false;
            }
        }

        if (Play_Mode == "GameOver")
        {
            rb.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
        }
    }

    private void EnableMakeCube()
    {
        make_cube_flag = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "bad_ball")
        {
            effect_audio.PlayOneShot(collision_sound);
            Life_num -= 1;
            heart_operation.Hurt(1);
            StartCoroutine(damage_effect(0.5f));
            Debug.Log("Miss");
            if (Life_num == 0)
            {
                Play_Mode = "GameOver";
                StartCoroutine(GameOver(1.3f));
                if (total_score > high_score)
                {
                    high_score = total_score;
                    PlayerPrefs.SetInt("SCORE", high_score);
                    PlayerPrefs.Save();//ディスクへの書き込み
                }
            }
        }

        else if (collision.gameObject.tag == "heart")
        {
            Debug.Log("recovery");
            if (Life_num <= 2)
            {
                Life_num += 1;
                heart_operation.Heal(1);
            }
            effect_audio.PlayOneShot(recover_sound);
        }
    }

    private void stick_move(float y_radiun)
    {
        if (north_left || north_right)
        {
            //effect_audio.PlayOneShot(run_sound);
            transform.position += new Vector3(Mathf.Sin(y_radiun), 0, Mathf.Cos(y_radiun)) * 0.05f;
        }

        else if (south_left || south_right)
        {
            //effect_audio.PlayOneShot(run_sound);
            transform.position -= new Vector3(Mathf.Sin(y_radiun), 0, Mathf.Cos(y_radiun)) * 0.05f;
        }

        else if (east_left || east_right)
        {
            //effect_audio.PlayOneShot(run_sound);
            y_radiun += 0.5f * pai;
            transform.position += new Vector3(Mathf.Sin(y_radiun), 0, Mathf.Cos(y_radiun)) * 0.05f;
        }

        else if (west_left || west_right)
        {
            //effect_audio.PlayOneShot(run_sound);
            y_radiun += 0.5f * pai;
            transform.position -= new Vector3(Mathf.Sin(y_radiun), 0, Mathf.Cos(y_radiun)) * 0.05f;
        }
    }

    private IEnumerator damage_effect(float waitminute)
    {
        Damage.SetActive(true);
        yield return new WaitForSeconds(waitminute);
        Damage.SetActive(false);
    }

    private IEnumerator GameOver(float waitminute)
    {
        yield return new WaitForSeconds(waitminute);
        SceneManager.LoadScene("Game Over Scene");
    }


}
