using System.Collections.Generic;
using UnityEngine;

public class HelicopterController : EnemyController {
    private float animationTime;
    private float refreshTarget;
    private float refreshTargetTime;
    private float secondaryAttackCooldown;
    private float currentSecondaryAttackTime;
    [SerializeField] private GameObject secondaryWeapon;
    [SerializeField] private GameObject secondaryWeaponSpawn;
    private int animationIndex;
    [SerializeField] private List<Sprite> sprites;
    private SpriteRenderer rend;
    private Vector2 targetPosition;

    private void Start() {
        attackCooldown = 0.2f;
        attackRange = 10.0f;
        currentAttackTime = 0.0f;
        moveSpeed = 6.0f;
        health = 35.0f;
        secondaryAttackCooldown = 6.0f;
        currentSecondaryAttackTime = 0.0f;

        refreshTarget = 1.0f;
        refreshTargetTime = 0.0f;

        rend = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        selectTarget();
        bodyAnimation();

        if(target != null) {
            move();
            attack();
        }
    }

    private void bodyAnimation() {
        animationTime += Time.deltaTime;

        if(animationTime > 0.25f) {
            animationTime = 0.0f;
            animationIndex = ++animationIndex >= sprites.Count ? 0 : animationIndex;

            rend.sprite = sprites[animationIndex];
        }
    }

    public override void attack() {
        currentAttackTime += Time.deltaTime;
        currentSecondaryAttackTime += Time.deltaTime;
        float x = transform.position.x - target.transform.position.x;

        if(x < attackRange) {
            if(currentAttackTime > attackCooldown) {
                HelicopterBullet bullet = Instantiate(weapon, weaponSpawn.transform.position, Quaternion.identity).GetComponent<HelicopterBullet>();
                bullet.setTarget(target.transform.position);
                currentAttackTime = 0.0f;
            }

            if(currentSecondaryAttackTime > secondaryAttackCooldown) {
                Instantiate(secondaryWeapon, secondaryWeaponSpawn.transform.position, Quaternion.identity);
                currentSecondaryAttackTime = 0.0f;
            }
        }
    }

    public override void damage(float amount) {
        health -= amount;

        if(health <= 0) destroy();
    }

    private void destroy() {
        Instantiate(destruction, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public override void move() {
        if(target != null) {
            if(Vector2.Distance(transform.position, targetPosition) < 0.5f) {
                targetPosition = new Vector2(target.transform.position.x + Random.Range(-3.0f, 3.0f), target.transform.position.y + Random.Range(2.0f, 6.0f));
            }

            Vector2 dir = (targetPosition - (Vector2)transform.position).normalized;
            transform.position += (Vector3)dir * moveSpeed * Time.deltaTime;
        }
    }
    
    public override void selectTarget() {
        if(target == null || refreshTargetTime > refreshTarget) {
            List<GameObject> robotPieces = RobotManager.pieces;
            float minDist = Mathf.Infinity;
            Vector2 pos = transform.position;

            foreach(GameObject piece in robotPieces) {
                if (piece != null) {
                    float dist = Vector2.Distance(pos, piece.transform.position);
                    if (dist < minDist)
                    {
                        minDist = dist;
                        target = piece;
                    }
                }
            }
        }
    }
}