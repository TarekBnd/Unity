using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{

    public int maxHealth;
    private int health;

    public int maxArmor;
    private int armor;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        armor = maxArmor;

        CanvasManager.Instance.UpdateHealth(health);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DamagePlayer(int damage)
    {
        if(armor > 0)
        {
            if (armor >= damage)
            {
                armor -= damage;
            } 
            else if(armor < damage)
            {
                int remaningDamage;
                remaningDamage = damage - armor;
                armor = 0;
                health -= remaningDamage;
            }
            
        } else {
            health-=damage;
        }

        if(health <= 0)
        {
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.buildIndex);
        }

        CanvasManager.Instance.UpdateHealth(health);
        CanvasManager.Instance.UpdateArmor(armor);

    }

    public void GiveHealth(int amount, GameObject pickup)
    {
        if(health < maxHealth)
        {
            health += amount;
            Destroy(pickup);
        }

        if(health > maxHealth)
        {
            health = maxHealth;
        }
        CanvasManager.Instance.UpdateHealth(health);
    }

    public void GiveArmor(int amount, GameObject pickup)
    {
        if(armor < maxArmor)
        {
            armor += amount;
            Destroy(pickup);
        }

        if(armor > maxArmor)
        {
            armor = maxArmor;
        }
        CanvasManager.Instance.UpdateHealth(armor);
    }
}
