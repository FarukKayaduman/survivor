using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Slider healthSlider;

    private void OnEnable()
    {
        EventManager.OnHealthUpdateEvent += UpdateHealthSlider;
    }
    private void OnDisable()
    {
        EventManager.OnHealthUpdateEvent -= UpdateHealthSlider;
    }

    private void UpdateHealthSlider(float health)
    {
        healthSlider.value = health;
    }

    private void UpdateScoreText()
    {

    }
}
