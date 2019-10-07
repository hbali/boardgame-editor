using Model;
using System;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public delegate void PlayerAccepted();

    public class AntiCheatUI : UIPopupComponent
    {
        private const string BASESTRING = "Password for {0}";
        [SerializeField] private RectTransform wrongPasswordUI;
        [SerializeField] private Text pName;
        [SerializeField] private InputField password;

        private Player player;

        public PlayerAccepted Accepted { get; internal set; }

        public void SetPlayer(Player p)
        {
            player = p;
            pName.text = String.Format(BASESTRING, p.PlayerName);
        }

        public override void Dispose()
        {
            password.text = "";
        }

        public void OnOk()
        {
            if(Validate())
            {
                WrongPassword(false);
                Accepted?.Invoke();
                UIComponentManager.Instance.RemoveComponent<AntiCheatUI>();
            }
            else
            {
                WrongPassword(true);
            }
        }

        private void WrongPassword(bool enable)
        {
            wrongPasswordUI.gameObject.SetActive(enable);
        }

        private bool Validate()
        {
            return string.IsNullOrEmpty(player.AntiCheat) || player.AntiCheat.Equals(password.text);
        }
    }
}