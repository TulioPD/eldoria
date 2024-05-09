using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private float parallaxEffect = 0.5f;

    private float startPositionX;
    private float spriteWidth;

    private void Start()
    {
        startPositionX = transform.position.x;
        spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void FixedUpdate()
    {
        float parallax = (mainCamera.transform.position.x - startPositionX) * parallaxEffect;
        float targetPosX = startPositionX + parallax;

        Vector3 newPosition = new Vector3(targetPosX, transform.position.y, transform.position.z);
        transform.position = newPosition;
    }
}

