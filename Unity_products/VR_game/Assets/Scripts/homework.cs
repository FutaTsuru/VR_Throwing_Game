using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Valve.VR;

public class homework : MonoBehaviour
{
    Rigidbody rb;

    AudioSource effect_audio;

    private float y_rotation;

    private float y_radiun;

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
    private Quaternion LeftHandRotationQ;
    //左コントローラの回転座標格納用
    private Vector3 LeftHandRotation;

    //右コントローラの位置座標格納用
    public Vector3 RightHandPosition;
    //右コントローラの回転座標格納用（クォータニオン）
    private Quaternion RightHandRotationQ;
    //右コントローラの回転座標格納用
    private Vector3 RightHandRotation;

    //InteractUIボタンが押されてるのかを判定するためのIuiという関数にSteamVR_Actions.default_InteractUIを固定
    private SteamVR_Action_Boolean Iui = SteamVR_Actions.default_InteractUI;

    private SteamVR_Action_Boolean A_button = SteamVR_Actions.default_click_A;

    private SteamVR_Action_Boolean B_button = SteamVR_Actions.default_click_B;

    private SteamVR_Action_Boolean north_stick = SteamVR_Actions.default_Teleport;

    private SteamVR_Action_Boolean east_stick = SteamVR_Actions.default_SnapTurnRight;

    private SteamVR_Action_Boolean west_stick = SteamVR_Actions.default_SnapTurnLeft;

    private SteamVR_Action_Boolean south_stick = SteamVR_Actions.default_snapturnback;

    private Boolean make_cube_flag = true;

    public Boolean interacrtu_left;

    public Boolean interacrtu_right;

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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        y_rotation = transform.localRotation.eulerAngles.y % 360;

        y_radiun = (y_rotation / 360) * 2 * pai;

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
        //位置座標を取得
        LeftHandPosition = InputTracking.GetLocalPosition(XRNode.LeftHand);

        //回転座標をクォータニオンで値を受け取る
        LeftHandRotationQ = InputTracking.GetLocalRotation(XRNode.LeftHand);
        //取得した値をクォータニオン → オイラー角に変換
        LeftHandRotation = LeftHandRotationQ.eulerAngles;
        //--------------------------------------------------------------


        //RightHand（右コントローラ）の情報を一時保管--------------------
        //位置座標を取得
        RightHandPosition = InputTracking.GetLocalPosition(XRNode.RightHand);
        //回転座標をクォータニオンで値を受け取る
        RightHandRotationQ = InputTracking.GetLocalRotation(XRNode.RightHand);
        //取得した値をクォータニオン → オイラー角に変換
        RightHandRotation = RightHandRotationQ.eulerAngles;

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

        if (interacrtu_left)
        {
            vibration.Execute(0, 0.2f, 100, 1, SteamVR_Input_Sources.LeftHand);
        }

        if (interacrtu_right)
        {
            vibration.Execute(0, 0.2f, 100, 1, SteamVR_Input_Sources.RightHand);
        }

        //左手のAボタン入力の際の処理（Cubeを左手コントローラーの位置に生成）
        if (a_left && make_cube_flag)
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            cube.transform.position = LeftHandPosition;
            make_cube_flag = false;
            Invoke(nameof(EnableMakeCube), 2);
        }

        //右手のAボタン入力の際の処理（Cubeを右手コントローラーの位置に生成）
        if (a_right && make_cube_flag)
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            cube.transform.position = RightHandPosition;
            make_cube_flag = false;
            Invoke(nameof(EnableMakeCube), 2);
        }

        //スティック処理
        if (north_left || north_right)
        {
            transform.position += new Vector3(Mathf.Sin(y_radiun), 0, Mathf.Cos(y_radiun)) * 0.05f;
        }

        else if (south_left || south_right)
        {
            transform.position -= new Vector3(Mathf.Sin(y_radiun), 0, Mathf.Cos(y_radiun)) * 0.05f;
        }

        else if (east_left || east_right)
        {
            y_radiun += 0.5f * pai;
            transform.position += new Vector3(Mathf.Sin(y_radiun), 0, Mathf.Cos(y_radiun)) * 0.05f;
        }

        else if (west_left || west_right)
        {
            y_radiun += 0.5f * pai;
            transform.position -= new Vector3(Mathf.Sin(y_radiun), 0, Mathf.Cos(y_radiun)) * 0.05f;
        }
    }

    private void EnableMakeCube()
    {
        make_cube_flag = true;
    }
}
