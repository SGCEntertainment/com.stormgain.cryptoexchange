using UnityEngine;

public class GameManager : MonoBehaviour
{
    public void SetDeal(int dir)
    {
        GameObject dealGo = Instantiate(Resources.Load<GameObject>("deal"));
        dealGo.transform.position = FindObjectOfType<Trail>().transform.position;

        Destroy(dealGo, 30.0f);
    }
}
