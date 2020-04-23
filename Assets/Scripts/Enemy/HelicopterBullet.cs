using UnityEngine;

public class HelicopterBullet : MonoBehaviour {
    private float moveSpeed = 12.0f;
    private float ttl = 5.0f;
    private float currentLife = 0.0f;
    private float damage = 0.3f;
    private Vector3 dir;

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.transform.tag == "RobotPiece") {
            IAttachable attachable = collision.gameObject.GetComponent<IAttachable>();
            if(attachable != null) {
                attachable.damage(damage);
            }
        }

        Destroy(gameObject);
    }

    void Update() {
        currentLife += Time.deltaTime;

        if(currentLife > ttl) {
            //explosion
            Destroy(gameObject);
        }

        transform.position += dir * moveSpeed * Time.deltaTime;
    }

    public void setTarget(Vector3 target) {
        dir = target - transform.position;
    }
}
