using BoardGameModels;
using Commands;
using Database;
using GridCreating;
using Model;
using Model.Builders;
using Providers.DataProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction;
using UI;
using UI.EventUI.Views;
using UnityEngine;
using UnityEngine.UI;

namespace UI.EventUI
{
    class EventTypeChooseView : UIPopupComponent
    {
        [SerializeField] Dropdown eventDropdown;
        [SerializeField] RuleLayout layout;
        [SerializeField] RectTransform layoutHolder;

        private Field mField;

        public override void Awake()
        {
            base.Awake();
            layoutHolder.gameObject.SetActive(false);
        }

        private void Start()
        {
            List<Dropdown.OptionData> datas = new List<Dropdown.OptionData>();
            for (int i = 0; i < FieldEventStructureProvider.Instance.Rules.Count; i++)
            {
                datas.Add(new Dropdown.OptionData(FieldEventStructureProvider.Instance.Rules[i].name));
            }
            eventDropdown.options = datas;
        }

        internal void Init(Field field)
        {
            mField = field;
        }

        public void OnNext()
        {
            layoutHolder.gameObject.SetActive(true);
            layout.LoadRule(FieldEventStructureProvider.Instance.Rules[eventDropdown.value]);
        }

        public void OnAdd()
        {
            FieldEvent evnt = layout.GetFieldEvent();
            mField = FieldBuilder.Edit(mField)
                .SetEvent(evnt)
                .SetType(mField.FType == FieldType.Start ? FieldType.Start : FieldType.Standard)
                .Change();

            CommandDispatcher.Instance.Execute<SaveBaseModelsCommand>(mField);
            OnCancel();
        }

        public void OnCancel()
        {
            Dispose();
            UIComponentManager.Instance.RemoveComponent<EventTypeChooseView>();
        }

        public override void Dispose()
        {
            layoutHolder.gameObject.SetActive(false);
            layout.Dispose();
        }
    }
}
