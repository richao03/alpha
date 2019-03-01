using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer: MonoBehaviour
{
    [SerializeField] int damage = 100;
// Start is called before the first frame update
public int GetDamage(){
    return damage;
}

// Update is called once per frame
public void Hit(){
    Destroy(gameObject);
}
}
