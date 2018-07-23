using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Market : MonoBehaviour
{
    public float wealtha; // all money offered
    public float wealthc;
    public float popc;
    public float foodo; // food offered
    public float foodd; // food demanded
    public float foodc;
    public int harvestmon = 5; //month of harvest
    GameObject[] cities;
    GameObject city;
    int month = 0;
    float foodprice;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (((int)Time.time - 1) % 5 == 0 && month != ((int)Time.time - 1) / 5 % 12 + 1)
        {
            int year = (int)(Time.time / 60);
            foodo = 0;
            foodd = 0;
            wealtha = 0;
            month = (int)Time.time / 5 % 12 + 1;
            if (month == harvestmon)
            {
                Debug.Log(year);
                Debug.Log("market is open");
                cities = GameObject.FindGameObjectsWithTag("City");
                for (int i = 0; i < cities.Length; i++)
                {
                    city = cities[i];
                    float pfood;
                    foodc = city.GetComponent<City>().food;
                    wealthc = city.GetComponent<City>().wealth;
                    popc = city.GetComponent<City>().pop;
                    pfood = foodc - (int)(popc * 12 * 1.4);
                    if (pfood < 0 && wealthc > 0)
                    {
                        foodd += pfood * -1;
                        int j = 1;
                        while (foodc - (int)(popc * (12 - j) * 1.4) < 0)
                        {
                            j++;
                        }
                        wealtha += wealthc * j / 12;
                    }
                    if (pfood > 0)
                    {
                        foodo += pfood;
                    }
                }
                // Debug.Log(foodo);
                // Debug.Log(foodd);
                // Debug.Log(wealtha);
                if (wealtha != 0 && foodo != 0)
                {
                    // Debug.Log("buy and sell folks");
                    float foodrate = foodo / foodd;
                    if (foodrate > 1)
                    {
                        foodprice = (wealtha / foodd / foodrate);
                    }
                    else
                    {
                        foodprice = (wealtha / foodo);
                    }
                    Debug.Log(foodprice);
                    for (int i = 0; i < cities.Length; i++)
                    {
                        city = cities[i];
                        foodc = city.GetComponent<City>().food;
                        wealthc = city.GetComponent<City>().wealth;
                        float pfood = foodc - (int)(popc * 12 * 1.4);
                        if (pfood < 0 && wealthc > 0)
                        {
                            if (foodrate > 1)
                            {
                                city.GetComponent<City>().food = foodc - (pfood / foodrate);
                                city.GetComponent<City>().wealth = wealthc + pfood * foodprice;
                            }
                            else
                            {
                                city.GetComponent<City>().food = foodc - (pfood);
                                city.GetComponent<City>().wealth = wealthc + pfood * foodrate * foodprice;
                            }
                        }
                        if (pfood > 0)
                        {
                            if (foodrate < 1)
                            {
                                city.GetComponent<City>().food = foodc - (pfood * foodrate);
                                city.GetComponent<City>().wealth = wealthc + pfood * foodprice;
                            }
                            else
                            {
                                city.GetComponent<City>().food = foodc - (pfood);
                                city.GetComponent<City>().wealth = wealthc + pfood * foodprice / foodrate;
                            }
                        }
                    }
                }
            }
        }
    }
}
