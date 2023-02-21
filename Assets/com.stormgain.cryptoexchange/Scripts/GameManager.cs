using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private const float timerCount = 30.0f;

    public enum DealType { Up, Down }
    [SerializeField] DealType dealType;

    private const float dealCount = 500.0f;

    private float Balance
    {
        get => PlayerPrefs.GetFloat("balance", 10000);
        set => PlayerPrefs.SetFloat("balance", value);
    }

    [Space(10)]
    [SerializeField] Text balanceText;
    [SerializeField] Text timerText;

    [Space(10)]
    [SerializeField] Button up;
    [SerializeField] Button down;

    [Space(10)]
    [SerializeField] Text winText;
    [SerializeField] Image direction;

    private void Start()
    {
        balanceText.text = $"BLC: {Balance:N} $";

        up.onClick.AddListener(() =>
        {
            SetDeal(DealType.Up);
        });

        down.onClick.AddListener(() =>
        {
            SetDeal(DealType.Down);
        });
    }

    private void SetDeal(DealType dealType)
    {
        if(Balance <= 0 || Balance - dealCount < 0)
        {
            return;
        }

        this.dealType = dealType;
        StartCoroutine(nameof(Dealing));

        direction.color = dealType == DealType.Up ? Color.green : Color.red;
        direction.transform.rotation = dealType == DealType.Up ? Quaternion.Euler(Vector3.zero) : Quaternion.Euler(Vector3.forward * 180);
        direction.gameObject.SetActive(true);

        GameObject dealGo = Instantiate(Resources.Load<GameObject>("deal"));
        dealGo.transform.position = FindObjectOfType<Trail>().transform.position;

        Destroy(dealGo, timerCount);
    }

    private IEnumerator Dealing()
    {
        up.interactable = false;
        down.interactable = false;

        Trail trail = FindObjectOfType<Trail>();
        float initTrailY = trail.transform.localPosition.y;

        float winCount = 0;

        float start = timerCount;
        while(start > 0)
        {
            start -= Time.deltaTime;

            winCount = dealCount * Mathf.Abs(initTrailY - trail.transform.localPosition.y);

            if (trail.transform.localPosition.y >= initTrailY && dealType == DealType.Up)
            {
                winText.text = $"{winCount:N} $";
                winText.color = Color.green;
            }
            else if (trail.transform.localPosition.y <= initTrailY && dealType == DealType.Down)
            {
                winText.text = $"{winCount:N} $";
                winText.color = Color.green;
            }
            else if (trail.transform.localPosition.y >= initTrailY && dealType == DealType.Down)
            {
                winText.text = $"-{winCount:N} $";
                winText.color = Color.red;
            }
            else if (trail.transform.localPosition.y <= initTrailY && dealType == DealType.Up)
            {
                winText.text = $"-{winCount:N} $";
                winText.color = Color.red;
            }

            int seconds = Mathf.RoundToInt(start % 60);
            timerText.text = $"{seconds}";

            yield return null;
        }

        direction.gameObject.SetActive(false);

        if (trail.transform.localPosition.y >= initTrailY && dealType == DealType.Up)
        {
            Balance += winCount;
        }
        else if(trail.transform.localPosition.y <= initTrailY && dealType == DealType.Down)
        {
            Balance += winCount;
        }
        else if (trail.transform.localPosition.y >= initTrailY && dealType == DealType.Down)
        {
            Balance -= winCount;
        }
        else if (trail.transform.localPosition.y <= initTrailY && dealType == DealType.Up)
        {
            Balance -= winCount;
        }

        balanceText.text = $"BLC: {Balance:N} $";
        winText.text = string.Empty;

        up.interactable = true;
        down.interactable = true;

        timerText.text = $"{timerCount}";
    }
}
