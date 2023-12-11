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
            float healthPercentage = necromancer.health / maxHealth;
            UpdateHealthBar(healthPercentage);
        }
    }

    private void UpdateHealthBar(float percentage)
    {
        healthBarFill.fillAmount = percentage;
        healthBarFill.rectTransform.localPosition = new Vector3((1 - percentage) * healthBarFill.rectTransform.sizeDelta.x / 2, 0, 0);
    }
}
