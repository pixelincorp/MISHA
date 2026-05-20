using MelonLoader;
using Il2Cpp;
using UnityEngine;

[assembly: MelonInfo(typeof(MISHAAutoChest.AutoChestMod), "AutoChest", "1.1.0", "Pixelincorp")]
[assembly: MelonGame("", "MISHA")]

namespace MISHAAutoChest
{
    public class AutoChestMod : MelonMod
    {
        private PlaytimeReward _reward;
        private GlobalKeyHook _keyHook;
        private float _timer = 0f;
        private float _clickTimer = 0f;
        private float _nextClickInterval = 0.8f;

        public override void OnUpdate()
        {
            // Автоклик каждые 0.8 сек в среднем (с рандомом от 0.6 до 1.0 сек)
            _clickTimer += Time.deltaTime;
            if (_clickTimer >= _nextClickInterval)
            {
                _clickTimer = 0f;
                _nextClickInterval = UnityEngine.Random.Range(0.6f, 1.0f);

                if (_keyHook == null)
                    _keyHook = UnityEngine.Object.FindObjectOfType<GlobalKeyHook>();

                if (_keyHook != null)
                    _keyHook.OnKeyPressed?.Invoke(0);
            }

            // Авто сундук
            _timer += Time.deltaTime;
            if (_timer < 1f) return;
            _timer = 0f;

            if (_reward == null)
            {
                _reward = UnityEngine.Object.FindObjectOfType<PlaytimeReward>();
                return;
            }

            if (_reward.CanClaim)
            {
                MelonLogger.Msg("Claiming reward!");
                _reward.Claim();
            }
        }
    }
}
