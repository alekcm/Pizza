using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public Transform movingObject, Back, SpawnPlace;
    public float Speed;
    public GameObject FoodPrefab, AddMoneyGO;
    public Player Player;
    public TextMeshProUGUI ScoreGO, WrongScoreGO, TimerText, MoneyText;
    public Image[] FoodImage;
    public Sprite[] FoodImages;
    public int NeededFood;

    //public AudioSource SoundSource;
    public AudioSource FailSound, TakeFoodSound, CompleteFoodSound; //3 источника звука

    public AudioClip TakeFood, CompleteFood, Fail; //3 мелодии

    public enum Food
    {
        Chicken,
        Olive,
        Bacon,
        Mushroom,
        Cheese,
        Egg,
        Tomato,
        Pepper,
        Fish
    }
    public Food[] AllowedFood = new Food[3];

    void Start()
    {
        Time.timeScale = 1f;
        //MoneyText.text = Convert.ToString(PlayerPrefs.GetInt("Money"));
        MoneyText.text = "0";
        StartNewPizza();
        StartCoroutine(SpawnFood());
        StartCoroutine(ChangeFood());
    }

    public void AddFood (Food food)
    {
        for (int i = 0; i < AllowedFood.Length; i++)
        {
            TakeFoodSound.clip = TakeFood;
            TakeFoodSound.Play();

            if (AllowedFood[i] == food)
            {
                ScoreGO.text = Convert.ToString(Convert.ToInt32(ScoreGO.text) - 1);
                
                if (Convert.ToInt32(ScoreGO.text) == 0)
                {
                    CompleteFoodSound.clip = CompleteFood;
                    CompleteFoodSound.Play();

                    int Money = NeededFood * 20 - (3 - Convert.ToInt32(WrongScoreGO.text)) * 10;
                    AddMoneyGO.GetComponent<TextMeshPro>().text = "+" + Convert.ToString(Money);
                    AddMoneyGO.GetComponent<TextMeshPro>().color = new Color(255,255,255);
                    AddMoneyGO.GetComponent<Animation>().Play();
                    PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money")+Money);
                    MoneyText.text = Convert.ToString(Convert.ToInt32(MoneyText.text) + Money);
                    StartNewPizza();
                }

                break;
            }
            if (i == AllowedFood.Length - 1)
            {
                WrongScoreGO.text = System.Convert.ToString(System.Convert.ToInt32(WrongScoreGO.text) - 1);
                if (Convert.ToInt32(WrongScoreGO.text) == 0)
                {
                    FailSound.clip = Fail;
                    FailSound.Play();

                    AddMoneyGO.GetComponent<TextMeshPro>().color = new Color(255,0,0);
                    AddMoneyGO.GetComponent<TextMeshPro>().text = "Заказ провален";
                    AddMoneyGO.GetComponent<Animation>().Play();
                    StartNewPizza();
                }
                
            }
        }
        
    }

    IEnumerator ChangeFood()
    {
        while(Convert.ToInt32(TimerText.text)>0)
        {
            TimerText.text = System.Convert.ToString(Convert.ToInt32(TimerText.text) - 1);
            yield return new WaitForSeconds(1);
        }
        AddMoneyGO.GetComponent<TextMeshPro>().color = new Color(255,0,0);
        AddMoneyGO.GetComponent<TextMeshPro>().text = "Время вышло";
        AddMoneyGO.GetComponent<Animation>().Play();

        FailSound.clip = Fail;
        FailSound.Play();

        StartNewPizza();
        StartCoroutine(ChangeFood());
    }

    IEnumerator SpawnFood()
    {
        Instantiate(FoodPrefab, new Vector2(SpawnPlace.position.x, SpawnPlace.position.y + Random.Range(-1f, 5f)),
            quaternion.identity);
        yield return new WaitForSeconds(Random.Range(0.25f, 1f));
        StartCoroutine(SpawnFood());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movingObject.position = new Vector2(movingObject.position.x + Speed, movingObject.position.y);
        Back.position = new Vector2(Back.position.x - Speed/50, Back.position.y);
    }

    public void StartNewPizza()
    {
        Player.Clean();
        int i = 0;
        while (i < AllowedFood.Length)
        {
            var v = Enum.GetValues(typeof(Food));
            int j = Random.Range(0, v.Length);
            AllowedFood[i] = (Food) v.GetValue(j);
            FoodImage[i].sprite = FoodImages[j];
            for (int k = 0; k <= i; k++)
            {
               if (k == i)
               {
                   i++;
                   break;
               }
                if (FoodImage[i].sprite == FoodImage[k].sprite)
                    break;
            }
        }
        NeededFood = Random.Range(3, 7);
        ScoreGO.text = Convert.ToString(NeededFood);
        WrongScoreGO.text = Convert.ToString(3);
        TimerText.text = Convert.ToString(NeededFood * 4 + Random.Range(-3, 5));
    }
}
