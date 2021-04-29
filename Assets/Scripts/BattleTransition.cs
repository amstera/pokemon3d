using UnityEngine;

public class BattleTransition : MonoBehaviour
{
    public float TimeToWaitSeconds;
    public float ScaleSpeed = 750;
    private float _timeSinceCreated;

    void OnEnable()
    {
        _timeSinceCreated = Time.time;
    }

    void Update()
    {
        if (enabled && Time.time - _timeSinceCreated >= TimeToWaitSeconds)
        {
            RectTransform rectTransform = GetComponent<RectTransform>();
            rectTransform.sizeDelta += new Vector2(ScaleSpeed * Time.deltaTime, 0);
        }
    }
}
