using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Stat _stat;

    void Init()
    {
        _stat = GetComponent<EnemyStat>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Init();        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // public MobTarget SelectTarget() { }

    void Attack()
    {
        Debug.Log("Attack!");
    }

    void AILogic()
    {
        
    }
}

abstract class Monster
{
    public abstract void Skill();
}
