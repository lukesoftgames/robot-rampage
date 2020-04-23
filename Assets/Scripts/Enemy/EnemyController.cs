using UnityEngine;

public class EnemyController : MonoBehaviour {
    protected float attackCooldown;
    protected float attackRange;
    protected float currentAttackTime;
    protected float health;
    protected float moveSpeed;
    protected GameObject target;
    [SerializeField] protected GameObject destruction;
    [SerializeField] protected GameObject weapon;
    [SerializeField] protected GameObject weaponSpawn;
    protected int moveDirection;

    public virtual void attack() {
        throw new System.NotImplementedException();
    }

    public virtual void damage(float amount) {
        throw new System.NotImplementedException();
    }

    public virtual void move() {
        throw new System.NotImplementedException();
    }

    public virtual void selectTarget() {
        throw new System.NotImplementedException();
    }
}