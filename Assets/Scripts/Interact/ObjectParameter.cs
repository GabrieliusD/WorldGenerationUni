using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ObjectParameter : MonoBehaviour
{
    public float maxHealth;
    public Slider slider;
    float health;

    void Start()
    {
        health = maxHealth;
        slider.value = health / maxHealth;
        if(tag == "Enemy")
        slider.fillRect.GetComponent<Image>().color = Color.red;
    }

    void Update()
    {
        if(slider!=null)
        {
        slider.value = health / maxHealth;
        slider.transform.rotation = Quaternion.Euler(90,0,0);//(transform.position + Camera.main.transform.forward);
        }
        if(health <= 0)
        Destroy(this.gameObject);
    }

    public void takeDamage(float amount)
    {
        health -= amount;
    }

}
