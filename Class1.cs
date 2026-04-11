using MelonLoader;
using Il2Cpp;
using UnityEngine;

[assembly: MelonInfo(typeof(MISHAAutoChest.AutoChestMod), "AutoChest", "1.0.0", "author")]
[assembly: MelonGame("", "MISHA")]

namespace MISHAAutoChest
{
    public class AutoChestMod : MelonMod
    {
        private PlaytimeReward _reward;
        private GlobalKeyHook _keyHook;
        private float _timer = 0f;

        public override void OnUpdate()
        {
            // Автоклик
            if (_keyHook == null)
                _keyHook = Object.FindObjectOfType<GlobalKeyHook>();

            if (_keyHook != null)
                _keyHook.OnKeyPressed?.Invoke(0);

            // Авто сундук
            _timer += Time.deltaTime;
            if (_timer < 1f) return;
            _timer = 0f;

            if (_reward == null)
            {
                _reward = Object.FindObjectOfType<PlaytimeReward>();
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