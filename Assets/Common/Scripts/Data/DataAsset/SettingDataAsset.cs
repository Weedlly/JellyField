using System;
using UnityEngine;

namespace Common.Scripts.Data.DataAsset
{
    public enum ETimeScaleType
    {
        Normal,
        Fast,
        VeryFast,
        Pause,
    }

    [Serializable]
    public struct SettingDataModel : IDefaultDataModel
    {
        public bool IsMusicOn;
        public bool IsSoundOn;
        public ETimeScaleType TimeScale;
        public bool IsEmpty()
        {
            return false;
        }
        public void SetDefault()
        {
            IsMusicOn = true;
            IsSoundOn = true;
        }
    }

    [CreateAssetMenu(fileName = "SettingDataAsset", menuName = "ScriptableObject/DataAsset/SettingDataAsset")]
    public class SettingDataAsset : LocalDataAsset<SettingDataModel>
    {
        [SerializeField] private ETimeScaleType _preTimeScale;
        public Action<bool> OnChangeMusic;
        public Action<bool> OnChangeSound;
        public bool IsMusicOn
        {
            get
            {
                return _model.IsMusicOn;
            }
            set
            {
                _model.IsMusicOn = value;
                OnChangeMusic?.Invoke(value);
                SaveData();
            }
        }
        public bool IsSoundOn
        {
            get
            {
                return _model.IsSoundOn;
            }
            set
            {
                _model.IsSoundOn = value;
                OnChangeSound?.Invoke(value);
                SaveData();
            }
        }

        public ETimeScaleType TimeScaleSetting
        {
            set
            {

                _preTimeScale = _model.TimeScale;
                _model.TimeScale = value;
                Time.timeScale = ConvertTimeScaleValue(_model.TimeScale);
                SaveData();
            }
            get
            {
                return _model.TimeScale;
            }
        }
        public ETimeScaleType PreTimeScaleSetting()
        {
            return _preTimeScale;
        }
        public float ConvertTimeScaleValue(ETimeScaleType timeScaleType)
        {
            switch (timeScaleType)
            {
                case ETimeScaleType.Pause: return 0f;
                case ETimeScaleType.Fast: return 1.5f;
                case ETimeScaleType.VeryFast: return 2f;
                // Normal case
                default: return 1f;
            }
        }
        public ETimeScaleType GetNextTimeScaleValue()
        {
            switch (_model.TimeScale)
            {
                case ETimeScaleType.Normal: return ETimeScaleType.Fast;
                case ETimeScaleType.Fast: return ETimeScaleType.VeryFast;
                case ETimeScaleType.VeryFast: return ETimeScaleType.Normal;
                default: return ETimeScaleType.Normal;
            }
        }
    }
}
