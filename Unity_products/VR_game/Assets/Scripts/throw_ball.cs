using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class throw_ball : MonoBehaviour
{
    [SerializeField] string whichHand;

    [SerializeField] AudioClip throw_sound;

    [SerializeField] AudioClip get_point_sound;

    [SerializeField] AudioClip lost_point_sound;

    AudioSource effect_audio;

    Rigidbody rb;

    pos pos_scripts;

    GameObject Player;

    GameObject LeftHand;

    GameObject RightHand;

    private Vector3 direction;

    private int max_power = 1000;

    private Vector3 hand_position;

    private float y_rotation;

    private float y_radian;

    private float x_rotation;

    private float pai = 3.14f;

    private Boolean throw_mode = false;

    private Boolean return_mode = false;

    private Boolean target_flag = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        effect_audio = GetComponent<AudioSource>();
        rb.isKinematic = true;
        Player = GameObject.Find("Player");
        LeftHand = GameObject.Find("LeftHand");
        RightHand = GameObject.Find("RightHand");
        pos_scripts = Player.GetComponent<pos>();
    }

    // Update is called once per frame
    void Update()
    {
        if (whichHand == "left" && !return_mode)
        {
            hand_position = LeftHand.transform.position;

            if (throw_mode && !pos_scripts.left_throw_mode)
            {
                return_mode = true;
                throw_mode = false;
                rb.isKinematic = false;

                //ボールを投げる処理
                throw_action(LeftHand, pos_scripts.LeftHandVelocity.magnitude);
                effect_audio.PlayOneShot(throw_sound);

                //ボールが返ってくる処理
                StartCoroutine(return_ball(3));
            }

            //ボールをつかんでいないときはtransformで処理(投球可能状態に遷移)
            else if (!pos_scripts.left_throw_mode)
            {
                rb.position = hand_position;
            }

            //ボールをつかんでからはrigidbodyで処理(投球可能状態)
            else
            {
                throw_mode = true;
                rb.position = hand_position;

            }
            
        }

        else if (whichHand == "right" && !return_mode) 
        {
            hand_position = RightHand.transform.position;

            if (throw_mode && !pos_scripts.right_throw_mode)
            {
                return_mode = true;
                throw_mode = false;
                rb.isKinematic = false;

                //ボールを投げる処理
                throw_action(RightHand, pos_scripts.RightHandVelocity.magnitude);
                effect_audio.PlayOneShot(throw_sound);

                //ボールが返ってくる処理
                StartCoroutine(return_ball(3));
            }

            //ボールをつかんでいないときはtransformで処理(投球可能状態に遷移)
            else if (!pos_scripts.right_throw_mode)
            {
                rb.position = hand_position;
            }

            //ボールをつかんでからはrigidbodyで処理(投球可能状態)
            else
            {
                throw_mode = true;
                rb.position = hand_position;
            }

        }
       
    }

    private IEnumerator return_ball(float waitminute)
    {
        yield return new WaitForSeconds(waitminute);
        rb.isKinematic = true;
        return_mode = false;
        target_flag = false;
    }

    private void throw_action(GameObject Hand, float velocity_maginitude)
    {
        y_rotation = Hand.transform.localRotation.y;

        if (y_rotation > 0)
        {
            y_rotation -= 0.6f * Mathf.Abs(y_rotation);
        }
        else
        {
            y_rotation += 0.6f * Mathf.Abs(y_rotation);
        }

        y_radian = y_rotation * pai;

        x_rotation = Hand.transform.localRotation.x;

        if (x_rotation < -0.5f)
        {
            x_rotation = (-x_rotation - 0.5f) * 0.9f;
        }

        else
        {
            x_rotation = (-0.5f - x_rotation) * 0.6f;
        }

        direction = new Vector3(Mathf.Cos(y_radian), 0.3f + x_rotation, -Mathf.Sin(y_radian));

        float power = decide_power(velocity_maginitude);

        rb.AddForce(direction * power);
    }

    private float decide_power(float velocity_maginitude)
    {

        if (velocity_maginitude > 5.5f)
        {
            return max_power;
        }

        return max_power * velocity_maginitude / 5.5f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //1度的に衝突したら、次の投球まで得点は加算されない
        if (!target_flag)
        {
            if (collision.gameObject.tag == "30")
            {
                pos.total_score += 30;
                effect_audio.PlayOneShot(get_point_sound);
                target_flag = true;
            }
            else if (collision.gameObject.tag == "50")
            {
                pos.total_score += 50;
                effect_audio.PlayOneShot(get_point_sound);
                target_flag = true;
            }
            else if (collision.gameObject.tag == "100")
            {
                pos.total_score += 100;
                effect_audio.PlayOneShot(get_point_sound);
                target_flag = true;
            }
            else if (collision.gameObject.tag == "150")
            {
                pos.total_score += 150;
                effect_audio.PlayOneShot(get_point_sound);
                target_flag = true;
            }
            else if (collision.gameObject.tag == "-150")
            {
                pos.total_score -= 150;
                effect_audio.PlayOneShot(lost_point_sound);
                target_flag = true;
            }
        }
        
    }
}
