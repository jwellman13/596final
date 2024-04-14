using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class SO_Enemy : ScriptableObject
{
    public int Health;
    public int Damage;
    public float Speed;
}
