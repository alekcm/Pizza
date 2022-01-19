using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuSceneManager : MonoBehaviour
{
    public TextMeshProUGUI money;

    public Transform MoneyTransform;

    public Transform[] points;
    public int k = 0;

    public GameObject[] NeededMoney;

    public Image[] Cupon;
    // Start is called before the first frame update
    void Start()
    {



        //  StartCoroutine(StartServer());
       
        if (PlayerPrefs.HasKey("Money"))
        {
            money.text = Convert.ToString(PlayerPrefs.GetInt("Money"));
        }
        else
        {
            PlayerPrefs.SetInt("Money", 0);
            money.text = "0";
        }

        int Money = PlayerPrefs.GetInt("Money");
        
        for (int i = 0; i < points.Length; i++)
        {
            if (Money < 500)
            {
                k = 0;
                break;
            }
            else if (Money < 1000)
            {
                k = 1;
                break;
            }
            else if (Money < 2500)
            {
                k = 2;
                break;
            }
            else if (Money < 5000)
            {
                k = 3;
                break;
            }
            else if (Money < 10000)
            {
                k = 4;
                break;
            }
            else if (Money < 20000)
            {
                k = 5;
                break;
            }
            else
            {
                k = 6;
            }
            
        }

        for (int i = k + 1; i < Cupon.Length; i++)
        {
            NeededMoney[i].SetActive(false);
            Cupon[i].color = new Color(255, 255, 255, 100);
            Cupon[i].gameObject.SetActive(false);
        }

        MoneyTransform.position = Vector3.Lerp(points[k].position, points[k+1].position, 1-(Money-Convert.ToInt32(points[k].name)/Convert.ToInt32(points[k+1].name)-Convert.ToInt32(points[k].name)));
    }

    IEnumerator StartServer()
    {
        WWWForm form = new WWWForm();
    form.AddField("Money", "0");
    WWW www = new WWW("http://alekcm8.ru/public_html/index.php", form);
    yield return www;
    if (www.error != null)
    {
        print(www.error);
        yield break;
    }
    Debug.Log("Сервер ответил " + www.text);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
