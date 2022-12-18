using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    //public Text healthText;
    public Image healthBar;

    float health, maxHealth = 100;
    //float lerpSpeed;

    private PlayerController player;

    private void OnEnable()
    {
        player = FindObjectOfType<PlayerController>();
    }

    private void Start()
    {
        health = maxHealth;
    }

    private void Update()
    {
        //healthText.text = "Health: " + health + "%";

        //lerpSpeed = 3f * Time.deltaTime;

        HealthBarFiller();
        ColorChange();

        if(health <= 0)
        {
            player.KillMe();
        }
    }

    void HealthBarFiller()
    {
        healthBar.fillAmount = health / maxHealth;
    }

    void ColorChange()
    {
        Color healthColor = Color.Lerp(Color.red, Color.green, (health / maxHealth));
        healthBar.color = healthColor;
    }

    public void Damage(float damagePoints)
    {
        health -= damagePoints;
    }
}
