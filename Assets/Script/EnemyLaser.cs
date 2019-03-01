using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
  // Start is called before the first frame update
  [SerializeField] float health = 100;

  private void OnTriggerEnter2D(Collider2D other)
  {
    DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
    if (!damageDealer) { return; }
    health -= damageDealer.GetDamage();
    if (health <= 0)
    {
      Destroy(gameObject);
    }
  }
}
