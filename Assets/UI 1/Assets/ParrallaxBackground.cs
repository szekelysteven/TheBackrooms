using UnityEngine;

public class ParrallaxBackground : MonoBehaviour
{

    Vector2 StartPosition;

    [SerializeField] int moveModifier;

    private void Start()
    {
        StartPosition = transform.position;
    }

    private void Update()
    {
        Vector2 pz = Camera.main.ScreenToViewportPoint(Input.mousePosition);

        float posX = Mathf.Lerp(transform.position.x, StartPosition.x + (pz.x * moveModifier), 2f * Time.deltaTime);
        float posY = Mathf.Lerp(transform.position.y, StartPosition.x + (pz.y * moveModifier), 2f * Time.deltaTime);

        transform.position = new Vector3(posX, posY, 0);
    }

}
