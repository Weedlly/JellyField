using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.TileGoal
{
    public class TileGoalView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textMeshPro;
        [SerializeField] private Image _image;
        public void Setup(Color tileColor, int tileAmountNeed)
        {
            _textMeshPro.text = tileAmountNeed.ToString();
            _image.color = tileColor;
        }
    }
}
