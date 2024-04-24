using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class delete_target : MonoBehaviour
{
    private float delete_time = 10;

    private float timeElapsed = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed > delete_time)
        {
            timeElapsed = 0;

            Destroy(this.gameObject);

            Debug.Log("delete target!!");
        }
    }
}
