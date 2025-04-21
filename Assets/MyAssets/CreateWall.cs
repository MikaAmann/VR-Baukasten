using UnityEngine;

public class Wall : MonoBehaviour
{
    public GameObject block;
    public int width = 10;
    public int height = 4;
    private Vector3 spawnPos;

    void Start()
    {
        spawnPos = transform.position;
    }
    
    public void createWall() 
    {
        for (int y = (int)spawnPos.y; y < height; ++y)
        {
            for (int x = (int)spawnPos.x; x < width; ++x)
            {
                Instantiate(block, new Vector3(x, y, spawnPos.z), Quaternion.identity);
            }
        }
    }
}