using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Attack", menuName = "Attack/Attack Data")]
public class AttackData_SO : ScriptableObject
{
    public float attackRange;//������������
    public float skillRange;//Զ�̹�������
    public float coolDown;//��ȴ
    public int minDamge;//��С�˺�
    public int maxDamge;//����˺�
    public float criticalMultiplier;//�����˺�
    public float criticalChance;//������
}
