using Newtonsoft.Json;
using System.IO;
using UnityEngine;

namespace Common.Scripts.Data.DataAsset
{
    public interface IDefaultDataModel
    {
        public bool IsEmpty();
        public void SetDefault();
    }

    public abstract class DataAsset : ScriptableObject
    {
        protected bool _isDoneLoadData;
        protected abstract void SaveData();

        public abstract void LoadData();

        public abstract bool IsDoneLoadData();
    }

    public abstract class LocalDataAsset<T> : DataAsset where T : struct, IDefaultDataModel
    {
        [SerializeField] private string _filename;
        [SerializeField] protected T _model;

        protected override void SaveData()
        {
            SaveLocalData(_filename, _model);
        }

        public override void LoadData()
        {
            LoadLocalData(_filename, out _model);
        }

        public override bool IsDoneLoadData()
        {
            return _isDoneLoadData;
        }


        // Check file exist function
        private bool IsFileExist(string filePath)
        {
            return File.Exists(filePath) && new FileInfo(filePath).Length > 0;
        }

        private string GetFilePath(string filename)
        {
            return Path.Combine(Application.persistentDataPath, filename);
        }
        private void SaveLocalData(string filename, T model)
        {
            string filePath = GetFilePath(filename);

            if (!IsFileExist(filePath))
            {
                model = new T();
                model.SetDefault();
            }

            string data = JsonConvert.SerializeObject(model);
            File.WriteAllText(filePath, data);
        }

        private void LoadLocalData(string filename, out T model)
        {
            _isDoneLoadData = false;
            string filePath = GetFilePath(filename);

            if (IsFileExist(filePath))
            {
                string jsonData = File.ReadAllText(filePath);
                model = JsonConvert.DeserializeObject<T>(jsonData);
                if (model.IsEmpty())
                {
                    model = new T();
                    model.SetDefault();
                    // Init data file with default value
                    SaveData();
                }
            }
            else
            {
                model = new T();
                model.SetDefault();
                // Init data file with default value
                SaveData();
            }
            _isDoneLoadData = true;
        }
    }
}
