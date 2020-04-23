using UnityEngine;

public class HelicopterBomb : MonoBehaviour {
    private float moveSpeed = 12.0f;
    private float ttl = 5.0f;
    private float currentLife = 0.0f;
    private float damage = 20.0f;
    [SerializeField] private GameObject impact;

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.transform != transform) {
            if(collision.transform.tag == "RobotPiece") {
                IAttachable attachable = collision.gameObject.GetComponent<IAttachable>();
                if(attachable != null) {
                    attachable.damage(damage);
                }
            }

            Instantiate(impact, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    void Update() {
        currentLife += Time.deltaTime;

        if(currentLife > ttl) {
            Instantiate(impact, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}