using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameManager GameManager;
    public SpriteRenderer sprite;
    public GameManager.Food SortOfFood;
    public int Power;
    
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager == null)
            GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        var v = System.Enum.GetValues(typeof(GameManager.Food));
        int j = Random.Range(0, v.Length);
        SortOfFood = (GameManager.Food)v.GetValue(j);
        sprite.sprite = GameManager.FoodImages[j];
        Power = Random.Range(100, 600);
    }
    
    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Floor" && !collider.gameObject.GetComponent<BoxCollider2D>().isTrigger)
        {
            rb.AddForce(Vector2.up * Power);
        }
        Destroy(gameObject, 6);//уничтожение еды через 3с
            //за 3 секунды она не успевает долетать до игрока. Я увеличил время.
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            collider.GetComponent<Player>().AddFood(sprite.sprite);
            GameManager.AddFood(SortOfFood);
            Destroy(rb.gameObject);
        }
    }
}
