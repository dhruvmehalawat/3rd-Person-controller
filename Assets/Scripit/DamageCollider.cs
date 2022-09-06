using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace move{
public class DamageCollider : MonoBehaviour
{
    Collider collider;

    public int currentWeponDamage =25;
    private void Awake() {
        collider = GetComponent<Collider>();
        collider.isTrigger = true;
        collider.gameObject.SetActive(true);
        collider.enabled = false;
    } 
    public void enableCollider(){
        collider.enabled = true;
    }
    public void DisableCollider(){
        collider.enabled = false;
    }
    private void OnTriggerEnter(Collider collision) {
        if(collision.tag == "Player"){
            PlayerStats playerStats = collision.GetComponent<PlayerStats>();
            if(playerStats != null){
                playerStats.DamageHealth(currentWeponDamage);
            }
        }
        if(collision.tag == "Enemy"){
            EnemyStats enemyStats = collision.GetComponent<EnemyStats>();
            if(enemyStats != null){
                enemyStats.EnemyTakeDamage(currentWeponDamage);
            }
        }
        
    }
}
}