using UnityEngine;

using UnityEngine;
public class Wall : MonoBehaviour
{
    public GameObject block;
    public int width = 10;
    public int height = 4;

    public void createWall()
    {
        for (int y = (int)transform.position.y; y < height; ++y)
        {
            for (int x = (int)transform.position.x; x < width; ++x)
            {
                Instantiate(block, new Vector3(x, y, transform.position.z), Quaternion.identity);
            }
        }
    }
}