using Common.Scripts.Data.DataAsset;
using SuperMaxim.Messaging;
using UnityEngine;

namespace Common.Scripts.Inventory
{
    public class CommonInventoryDataViewModel : MonoBehaviour
    {
        [SerializeField] private InventoryType _inventoryType;
        [SerializeField] private CommonInventoryDataView _inventoryDataView;
        [SerializeField] private InventoryDataAsset _inventoryDataAsset;
        private void Start()
        {
            SubscribeAmountChange();
            SetUpView();
        }
        private void OnDestroy()
        {
            UnSubscribeAmountChange();
        }
        private void OnInventoryChange(InventoryChangePayload payload)
        {
            SetUpView();
        }
        private void SetUpView()
        {
            InventoryData inventoryData = _inventoryDataAsset.GetInventoryDataByType(_inventoryType);
            _inventoryDataView.Setup(inventoryData.Amount);
        }
        private void SubscribeAmountChange()
        {
            Messenger.Default.Subscribe<InventoryChangePayload>(OnInventoryChange, payload => payload.InventoryType == _inventoryType);
        }
        private void UnSubscribeAmountChange()
        {
            Messenger.Default.Subscribe<InventoryChangePayload>(OnInventoryChange, payload => payload.InventoryType == _inventoryType);
        }
    }
}
