using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace move{
public class Interectable : MonoBehaviour
{
    public float radius = 0.6f;
    public string InterectableText;
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
    public virtual void Interact(PlayerManger playerManger){
        Debug.Log("picked up a item");

    }
}
}