using UnityEngine;

public class PlayerEntity : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        CheckCollision(other.gameObject);
    }

    private void OnCollisionEnter(Collision other) {
        CheckCollision(other.gameObject);
    }

    private void CheckCollision(GameObject other) {
        if(other.CompareTag("Dangerous")) {
            GameManager.Instance.GameOver();
            var entities = FindObjectsByType<PlayerEntity>(FindObjectsSortMode.None);
            foreach (var entity in entities)
            {
                entity.gameObject.SetActive(false);
            }
        }
    }
}
