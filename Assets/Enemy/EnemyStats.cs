using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace move{
public class EnemyStats : MonoBehaviour
{
    public int enemyHitpoint = 10;
    public int EnemyCurrentHealth;
    public int enmyMaxHealth;

    Animator anim;
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
       enmyMaxHealth= setMaxHealthformenemyhealthpoints();
       EnemyCurrentHealth =enmyMaxHealth;
    }
    public void EnemyTakeDamage(int damage){
        anim.Play("Hit");
        EnemyCurrentHealth = EnemyCurrentHealth-damage;
        if(EnemyCurrentHealth == 0){
            EnemyCurrentHealth =0;
            anim.Play("Death");
        }
    }
    private int setMaxHealthformenemyhealthpoints(){
        enmyMaxHealth = enemyHitpoint * 10;
        return enmyMaxHealth;
    }
}
}