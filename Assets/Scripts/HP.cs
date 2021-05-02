using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    public float Health = 20;
    public float MaxHealth = 20;
    private Slider _slider;
    private bool _updateBar;

    void Start()
    {
        _slider = GetComponent<Slider>();
    }

    void Update()
    {
        if (_updateBar && _slider.value > Health/MaxHealth)
        {
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
        Invoke("HitDelay", 0.25f);
    }

    private void HitDelay()
    {
        _updateBar = true;
    }
}
