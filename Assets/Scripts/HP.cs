using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    public float Health = 20;
    public float MaxHealth = 20;
    public Image Fill;
    private Slider _slider;
    private bool _updateBar;
    private Color _startColor;

    void Start()
    {
        _slider = GetComponent<Slider>();
        _startColor = Fill.color;
    }

    void Update()
    {
        if (_updateBar && _slider.value > Health/MaxHealth)
        {
            Fill.color = Color.Lerp(Color.red, _startColor, _slider.value);
            _slider.value -= Time.deltaTime/2;
            if (_slider.value <= Health / MaxHealth)
            {
                _updateBar = false;
            }
        }
    }

    public void TakeHit(int amount)
    {
        Health -= amount;
        Invoke("HitDelay", 0.35f);
    }

    private void HitDelay()
    {
        _updateBar = true;
    }
}
