using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private const float dealCount = 500.0f;

    private float Balance
    {
        get => PlayerPrefs.GetFloat("balance", 10000);
        set => PlayerPrefs.SetFloat("balance", value);
    }

    [SerializeField] Text balanceText;
    [SerializeField] Text timerText;

    [Space(10)]
    [SerializeField] Button up;
    [SerializeField] Button down;

    private void Start()
    {
        balanceText.text = $"BLC: {Balance:N} $";

        up.onClick.AddListener(() =>
        {
            SetDeal(1);
        });

        down.onClick.AddListener(() =>
        {
            SetDeal(-1);
        });
    }

    private void SetDeal(int dir)
    {
        if(Balance <= 0 || Balance - dealCount < 0)
        {
            return;
        }

        Balance -= dealCount;
        balanceText.text = $"BLC: {Balance:N} $";
        StartCoroutine(nameof(Dealing));

        GameObject dealGo = Instantiate(Resources.Load<GameObject>("deal"));
        dealGo.transform.position = FindObjectOfType<Trail>().transform.position;

        Destroy(dealGo, 30.0f);
    }

    private IEnumerator Dealing()
    {
        up.interactable = false;
        down.interactable = false;

        float start = 30.0f;
        while(start > 0)
        {
            start -= Time.deltaTime;

            int seconds = Mathf.RoundToInt(start % 60);
            timerText.text = $"{seconds}";

            yield return null;
        }

        up.interactable = true;
        down.interactable = true;

        timerText.text = $"{30}";
    }
}
