using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHealthBar : MonoBehaviour
{
    public NecromancerMovement necromancer;
    public Image healthBarFill;

    private float maxHealth;

    void Start()
    {
        if (necromancer != null)
        {
            maxHealth = necromancer.health;
        }
    }

    void Update()
    {
        if (necromancer != null)
        {
            float healthPercentage = Mathf.Max(0, necromancer.health / maxHealth);
            UpdateHealthBar(healthPercentage);
        }
    }

    private void UpdateHealthBar(float percentage)
    {
        healthBarFill.rectTransform.transform.localScale = new Vector3(percentage, 1, 1);
    }
}
