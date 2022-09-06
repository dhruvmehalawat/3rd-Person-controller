using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace move{
public class damagePlayer : MonoBehaviour
{
   public int damage = 25;
   private void OnTriggerEnter(Collider other) {
    PlayerStats playerStats = other.GetComponent<PlayerStats>();
    if(playerStats != null){
        playerStats.DamageHealth(damage);
    }
   }
}
}