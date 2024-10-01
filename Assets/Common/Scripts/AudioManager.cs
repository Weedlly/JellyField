using Common.Scripts.Data.DataAsset;
using Cysharp.Threading.Tasks;
using SuperMaxim.Messaging;
using UnityEngine;

namespace Common.Scripts
{
    public struct AudioPlayOneShotPayload
    {
        public AudioClip AudioClip;
    }

    public struct AudioPlayLoopPayload
    {
        public AudioClip AudioClip;
    }

    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSourceLoop;
        [SerializeField] private AudioSource _audioSourceOneShot;
        [SerializeField] private SettingDataAsset _settingDataAsset;
        private async void Awake()
        {
            Messenger.Default.Subscribe<AudioPlayOneShotPayload>(PlayOneShot);
            Messenger.Default.Subscribe<AudioPlayLoopPayload>(PlayLoop);
            _settingDataAsset.OnChangeSound += OnChangeSound;
            _settingDataAsset.OnChangeMusic += OnChangeMusic;

            await UniTask.WaitUntil(() => _settingDataAsset.IsDoneLoadData());
            OnChangeSound(_settingDataAsset.IsSoundOn);
            OnChangeMusic(_settingDataAsset.IsMusicOn);
        }
        private void OnDestroy()
        {
            Messenger.Default.Unsubscribe<AudioPlayOneShotPayload>(PlayOneShot);
            Messenger.Default.Unsubscribe<AudioPlayLoopPayload>(PlayLoop);
            _settingDataAsset.OnChangeSound -= OnChangeSound;
            _settingDataAsset.OnChangeMusic -= OnChangeMusic;
        }
        private void OnChangeSound(bool isTurnOn)
        {
            _audioSourceOneShot.mute = !isTurnOn;
        }
        private void OnChangeMusic(bool isTurnOn)
        {
            _audioSourceLoop.mute = !isTurnOn;
        }
        private void PlayOneShot(AudioPlayOneShotPayload payload)
        {
            _audioSourceOneShot.PlayOneShot(payload.AudioClip);
        }
        private void PlayLoop(AudioPlayLoopPayload payload)
        {
            _audioSourceLoop.clip = payload.AudioClip;
            _audioSourceLoop.loop = true;
            _audioSourceLoop.Play();
        }
    }
}
