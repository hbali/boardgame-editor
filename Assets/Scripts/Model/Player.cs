using Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction;
using View;
using View.Builders;

namespace Model
{
    public class Player : BaseModel
    {
        public string FieldId { get; set; }

        public Field CurrentField { get { return _repo.GetModel<Field>(FieldId); } }

        public int CurrencyAmount { get; set; }
        public string ModelPath { get; set; }
        public Dictionary<string, int> Items { get; set; }
        public string PlayerName { get; set; }
        public string AntiCheat { get; set; }

        public PlayerView PView
        {
            get { return View as PlayerView; }
        }

        public string PrefabPath
        {
            get
            {
                return "Prefabs/PlayerModels/Models/" + ModelPath;
            }
        }

        public int LeftOut { get; set; }

        public override void LoadModel()
        {
            if (View == null)
            {
                View = PlayerViewBuilder.In().SetPlayer(this).Build();
            }
            else
            {
                PView.LoadModel();
            }
        }

        public override Memento GetSnapshot(bool deleted = false)
        {
            return new DbPlayer()
            {
                playerName = PlayerName,
                Id = Id,
                fieldId = FieldId,
                currencyAmount = CurrencyAmount,
                model = ModelPath,
                anticheat = AntiCheat,
                leftout = LeftOut
            };
        }

        public override void LoadSnapshot(Memento m)
        {
            DbPlayer p = m as DbPlayer;
            FieldId = p.fieldId;
            CurrencyAmount = p.currencyAmount;
            ModelPath = p.model;
            Id = p.Id;
            Items = p.items ?? new Dictionary<string, int>();
            PlayerName = p.playerName;
            AntiCheat = p.anticheat;
            LeftOut = p.leftout;
        }

        public override void LoadDependantFields()
        {

        }
    }
}
