using Underlunchers.Items;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Underlunchers.UI.Slots
{
    [RequireComponent(typeof(EventTrigger))]
    public abstract class ItemSlot : UISlot
    {
        public bool IsEmpty { get { return Content == null; } }
        public Collectable Content { get; protected set; }

        public delegate void OnRightClickedHandler(Collectable collectable);
        public event OnRightClickedHandler OnRightClicked;

        protected virtual void Awake()
        {
            var trigger = GetComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback.AddListener((data) => OnPointerClick((PointerEventData)data));
            trigger.triggers.Add(entry);
        }

        public void SetContent(Collectable collectable)
        {
            Content = collectable;
            SetIcon(collectable.Icon);
        }

        public void SetEmpty()
        {
            Content = null;
            SetIcon(null);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right && OnRightClicked != null)
            {
                OnRightClicked(Content);
            }
        }
    }
}