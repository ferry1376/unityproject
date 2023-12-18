using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] 
    private float enemyHealth;
    private float enemyMaxHealth;


    private float lerpTimer;

    // Start is called before the first frame update
    void Start()
    {
        enemyHealth = enemyMaxHealth;
        
    }

    // Update is called once per frame
    void Update()
    {
        enemyHealth = Mathf.Clamp(enemyHealth, 0, enemyMaxHealth);

        
    }
    public void enemyTakeDamage(float enemyDamage)
    {
        Debug.Log(enemyHealth);
        enemyHealth -= enemyDamage;
        if (enemyHealth <= 0)
        {
            // Gain experience when enemy dies
            LevelSystem levelSystem = FindObjectOfType<LevelSystem>();
            if (levelSystem != null)
            {
            levelSystem.GainExperienceFlatRate(10); // Or any other value you deem appropriate
            }
            Destroy(gameObject);
        }
        lerpTimer = 0f;
        
        

    }
}
