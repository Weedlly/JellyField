using TMPro;
using UnityEngine;

namespace Common.Scripts.Inventory
{
    public class CommonInventoryDataView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _txtAmount;
        public void Setup(float amount)
        {
            _txtAmount.text = amount.ToString();
        }
    }
}
