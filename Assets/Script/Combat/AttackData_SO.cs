using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Attack", menuName = "Attack/Attack Data")]
public class AttackData_SO : ScriptableObject
{
    public float attackRange;//»ù´¡¹¥»÷¾àÀë
    public float skillRange;//Ô¶³Ì¹¥»÷¾àÀë
    public float coolDown;//ÀäÈ´
    public int minDamge;//×îĞ¡ÉËº¦
    public int maxDamge;//×î´óÉËº¦
    public float criticalMultiplier;//±©»÷ÉËº¦
    public float criticalChance;//±©»÷ÂÊ
}
