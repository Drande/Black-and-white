using UnityEngine;

public class CoinRotation : MonoBehaviour
{   public int value = 1;
    public float rotationSpeed = 100f; // Velocidad de rotación

    void Update()
    {
        // Rotar la moneda alrededor del eje Y
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}

