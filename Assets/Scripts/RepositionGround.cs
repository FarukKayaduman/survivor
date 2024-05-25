using Characters;
using UnityEngine;

public class RepositionGround : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("PlayerArea"))
            return;

        Vector3 playerPosition = Player.Instance.transform.position;
        Vector3 myPosition = transform.position;

        const int tileMoveAmount = 48;
        
        float directionX = playerPosition.x - myPosition.x;
        float directionY = playerPosition.y - myPosition.y;
        
        float diffX = Mathf.Abs(directionX);
        float diffY = Mathf.Abs(directionY);

        directionX = directionX > 0 ? 1 : -1;
        directionY = directionY > 0 ? 1 : -1;
        
        switch (transform.tag)
        {
            case "Ground":
                if (diffX > diffY)
                    transform.Translate(Vector3.right * directionX * tileMoveAmount);
                else if (diffX < diffY)
                    transform.Translate(Vector3.up * directionY * tileMoveAmount);
                break;
            case "Enemy":

                break;
        }
    }
}
