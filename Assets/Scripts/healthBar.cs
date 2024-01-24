using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{

    private const float maxHealth = 100f;

    public  static float health = maxHealth;

    private Image bar;
    // Start is called before the first frame update
    void Start()
    {
        bar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        bar.fillAmount = health / maxHealth;
    }
}