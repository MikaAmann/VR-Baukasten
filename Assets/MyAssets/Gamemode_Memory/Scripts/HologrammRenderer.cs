using UnityEngine;

public class HologrammRenderer : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject[] HoloTowerList;


    public void DisplayHoloTower(int pos)
    {
        Instantiate(HoloTowerList[pos], spawnPoint);
    }
    
}
