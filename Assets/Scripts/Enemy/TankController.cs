using System.Collections.Generic;
using UnityEngine;

public class TankController : EnemyController {
    private bool flash = false;
    private float flashTime = 0.0f;
    private int flashIndex = 0;
    [SerializeField] GameObject barrel;
    private GameObject currentFlash;
    [SerializeField] private List<GameObject> muzzleFlash;


    private void Start() {
        attackCooldown = 3.0f;
        attackRange = 6.0f;
        currentAttackTime = 0.0f;
        moveSpeed = 1.0f;
        health = 100.0f;
    }

    private void Update() {
        selectTarget();

        if(target != null) {
            move();
            rotateTurret();
            attack();
        }

        flashAnimation();
    }

    public override void attack() {
        currentAttackTime += Time.deltaTime;

        if(currentAttackTime > attackCooldown) {
            float x = transform.position.x - target.transform.position.x;

            if(x < attackRange) {
                Instantiate(weapon, weaponSpawn.transform.position, barrel.transform.rotation);
                flash = true;
                currentAttackTime = 0.0f;
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

    private void flashAnimation() {
        if(flash) {
            if(flashTime > 0.5f) {
                flash = false;
                flashTime = 0.0f;
                flashIndex = 0;
                Destroy(currentFlash);
            } else if(flashIndex == 0) {
                currentFlash = Instantiate(muzzleFlash[flashIndex], weaponSpawn.transform.position, barrel.transform.rotation);
                flashIndex++;
            } else if(flashTime > 0.25f && flashIndex == 1) {
                Destroy(currentFlash);
                currentFlash = Instantiate(muzzleFlash[flashIndex], weaponSpawn.transform.position, barrel.transform.rotation);
                flashIndex++;
            }

            flashTime += Time.deltaTime;

            Vector3 euler = currentFlash.transform.eulerAngles;
            Vector3 barrelEuler = barrel.transform.eulerAngles;
            currentFlash.transform.rotation = Quaternion.Euler(euler.x, euler.y, barrelEuler.z);


        }
    }

    public override void move() {
        float x = Mathf.Infinity;
        if(target != null) x = transform.position.x - target.transform.position.x;

        if(x > attackRange) {
            Vector3 pos = transform.position;

            pos.x -= moveSpeed * Time.deltaTime;

            transform.position = pos;
        }
    }

    private void rotateTurret() {
        float x = transform.position.x - target.transform.position.x;
        float y = transform.position.y - target.transform.position.y;

        float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg * -1;

        Vector3 euler = barrel.transform.eulerAngles;
        barrel.transform.rotation = Quaternion.Euler(euler.x, euler.y, 360 - angle);
    }

    public override void selectTarget() {
        if(target != null) return;

        List<GameObject> robotPieces = RobotManager.pieces;
        float minDist = Mathf.Infinity;
        Vector2 pos = transform.position;

        foreach(GameObject piece in robotPieces) {
            if (piece != null)
            {
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