using CustomInspector;
using SuperMaxim.Messaging;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Scripts.Data.DataAsset
{
    public enum InventoryType
    {
        TotalStar = 1, // Total Star of all Stage are conquered
        TalentPoint = 2, // Point use to upgrade Rune
        GoldenCoin = 3, // Can be placed when purchase
        SliverCoin = 4, // Can be placed after complete each Stage
    }

    [Serializable]
    public struct InventoryData
    {
        public InventoryType InventoryType;
        public int Amount;
    }

    [Serializable]
    public struct InventoryDataModel : IDefaultDataModel
    {
        public List<InventoryData> ListInventoryData;
        public bool IsEmpty()
        {
            return false;
        }
        public void SetDefault()
        {
            ListInventoryData = new List<InventoryData>
            {
                new InventoryData
                {
                    InventoryType = InventoryType.TotalStar,
                    Amount = 0,
                },
                new InventoryData
                {
                    InventoryType = InventoryType.TalentPoint,
                    Amount = 0,
                },
                new InventoryData
                {
                    InventoryType = InventoryType.GoldenCoin,
                    Amount = 0,
                },
                new InventoryData
                {
                    InventoryType = InventoryType.SliverCoin,
                    Amount = 0,
                },
            };
        }
    }

    [CreateAssetMenu(fileName = "InventoryDataAsset", menuName = "ScriptableObject/DataAsset/InventoryDataAsset")]
    public class InventoryDataAsset : LocalDataAsset<InventoryDataModel>
    {
        public List<InventoryData> InventoryDatas
        {
            get
            {
                return _model.ListInventoryData ??= new List<InventoryData>();
            }
        }
        public void TryChangeInventoryData(InventoryType type, int amountChange)
        {
            for (int i = 0; i < InventoryDatas.Count; i++)
            {
                if (InventoryDatas[i].InventoryType != type)
                    continue;
                InventoryData updatedInventory = InventoryDatas[i];
                updatedInventory.Amount += amountChange;
                InventoryDatas[i] = updatedInventory; // Reassign the modified struct back to the list

                SaveData();

                NotifyAmountChange(type);
                break;
            }
        }
        private static void NotifyAmountChange(InventoryType inventoryType)
        {
            Messenger.Default.Publish(new InventoryChangePayload
            {
                InventoryType = inventoryType,
            });
        }
        public InventoryData GetInventoryDataByType(InventoryType type)
        {
            return InventoryDatas.Find(data => data.InventoryType == type);
        }
        public List<InventoryData> GetAllStageData()
        {
            return InventoryDatas;
        }
#if UNITY_EDITOR
        [Button("TestTryChangeInventoryData")]
        public InventoryType TestInventoryType;
        public int TestAmountChange;
        public void TestTryChangeInventoryData()
        {
            TryChangeInventoryData(TestInventoryType, TestAmountChange);
        }
#endif
    }

    public struct InventoryChangePayload
    {
        public InventoryType InventoryType;
    }
}
