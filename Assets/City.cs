using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour
{
    public float pop;
    public float idealpop;
    public float farmeffper; // farm efficentcy percent
    public float food;
    public Color color = new Color(0.2F, 0.3F, 0.4F, 0.5F);
    public float wealth;
    int month = 0;
    //string[] monthn = ["Jan", "Feb", "Mar", "Apr", "May", "June", "July", "Aug", "Sept", "Oct", "Nov", "Dec"];
    // Use this for initialization
    void Start()
    {
        GetComponent<Renderer>().material.color = new Color(0, 255, 0);
    }

    // Update is called once per frame
    void Update()
    {

        // 5 seconds is a month does this every month
        if ((int)Time.time % 5 == 0 && month != (int)Time.time / 5 % 12 + 1)
        {
            month = (int)Time.time / 5 % 12 + 1;
            //Debug.Log(month);\
            // yeah new people
            if (food > 0)
            {
                pop += 100 * (idealpop / (idealpop + pop));
            }
            // people eat food
            food -= pop;
            if (month == 5)
            {
                food += (int)((farmeffper + 100) * pop * 12 / 100 * (Random.value / 2 + 0.25) * 2);
            }
            // can't have negitive food
            if (food < 0)
            {
                food = 0;
            }
            // people die if no food
            if (food == 0)
            {
                pop = pop / 10 * 9;
            }
            // if their is not enough people the city dies
            if (pop < 1000)
            {
                Destroy(this.gameObject);
            }
            // colors city based off performance
            if (wealth < 0 || food < 0)
            {
                GetComponent<Renderer>().material.color = new Color(255, 0, 0);
            }
            else
            {
                GetComponent<Renderer>().material.color = new Color(0, 255, 0);
            }
        }
    }
}
