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

    //HMD�̈ʒu���W�i�[�p
    private Vector3 HMDPosition;
    //HMD�̉�]���W�i�[�p�i�N�H�[�^�j�I���j
    private Quaternion HMDRotationQ;
    //HMD�̉�]���W�i�[�p�i�I�C���[�p�j
    private Vector3 HMDRotation;

    //���R���g���[���̈ʒu���W�i�[�p
    public Vector3 LeftHandPosition;
    //���R���g���[���̉�]���W�i�[�p�i�N�H�[�^�j�I���j
    private Quaternion LeftHandRotationQ;
    //���R���g���[���̉�]���W�i�[�p
    private Vector3 LeftHandRotation;

    //�E�R���g���[���̈ʒu���W�i�[�p
    public Vector3 RightHandPosition;
    //�E�R���g���[���̉�]���W�i�[�p�i�N�H�[�^�j�I���j
    private Quaternion RightHandRotationQ;
    //�E�R���g���[���̉�]���W�i�[�p
    private Vector3 RightHandRotation;

    //InteractUI�{�^����������Ă�̂��𔻒肷�邽�߂�Iui�Ƃ����֐���SteamVR_Actions.default_InteractUI���Œ�
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

    //�U���̓��͂̂��߂�vibration�Ƃ����֐���SteamVR_Actions.default_Haptic���Œ�
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

        /*InputTracking.GetLocalPosition(XRNode.�@�햼)�ŋ@��̈ʒu��������Ăяo����*/

        //Head�i�w�b�h�}�E���h�f�B�X�v���C�j�̏����ꎞ�ۊ�-----------
        //�ʒu���W���擾
        HMDPosition = InputTracking.GetLocalPosition(XRNode.Head);
        //��]���W���N�H�[�^�j�I���Œl���󂯎��
        HMDRotationQ = InputTracking.GetLocalRotation(XRNode.Head);
        //�擾�����l���N�H�[�^�j�I�� �� �I�C���[�p�ɕϊ�
        HMDRotation = HMDRotationQ.eulerAngles;
        //--------------------------------------------------------------


        //LeftHand�i���R���g���[���j�̏����ꎞ�ۊ�--------------------
        //�ʒu���W���擾
        LeftHandPosition = InputTracking.GetLocalPosition(XRNode.LeftHand);

        //��]���W���N�H�[�^�j�I���Œl���󂯎��
        LeftHandRotationQ = InputTracking.GetLocalRotation(XRNode.LeftHand);
        //�擾�����l���N�H�[�^�j�I�� �� �I�C���[�p�ɕϊ�
        LeftHandRotation = LeftHandRotationQ.eulerAngles;
        //--------------------------------------------------------------


        //RightHand�i�E�R���g���[���j�̏����ꎞ�ۊ�--------------------
        //�ʒu���W���擾
        RightHandPosition = InputTracking.GetLocalPosition(XRNode.RightHand);
        //��]���W���N�H�[�^�j�I���Œl���󂯎��
        RightHandRotationQ = InputTracking.GetLocalRotation(XRNode.RightHand);
        //�擾�����l���N�H�[�^�j�I�� �� �I�C���[�p�ɕϊ�
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

        //�����A�{�^�����͂̍ۂ̏����iCube������R���g���[���[�̈ʒu�ɐ����j
        if (a_left && make_cube_flag)
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            cube.transform.position = LeftHandPosition;
            make_cube_flag = false;
            Invoke(nameof(EnableMakeCube), 2);
        }

        //�E���A�{�^�����͂̍ۂ̏����iCube���E��R���g���[���[�̈ʒu�ɐ����j
        if (a_right && make_cube_flag)
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            cube.transform.position = RightHandPosition;
            make_cube_flag = false;
            Invoke(nameof(EnableMakeCube), 2);
        }

        //�X�e�B�b�N����
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
