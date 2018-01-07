using Simulator.Items;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Simulator.UI.Slots
{
    [RequireComponent(typeof(EventTrigger))]
    public abstract class ItemSlot : UISlot
    {
        public bool IsEmpty { get { return Content == null; } }
        public Collectable Content { get; protected set; }

        protected override void Awake()
        {
            base.Awake();
            var trigger = GetComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback.AddListener((data) => OnPointerClick((PointerEventData)data));
            trigger.triggers.Add(entry);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                OnRightClick();
            }
        }

        internal void Add(Collectable collectable)
        {
            Content = collectable;
            SetIcon(collectable.Icon, Color.white);
        }

        protected void EmptySlot()
        {
            Content = null;
            SetIcon(null);
        }

        protected abstract void OnRightClick();
    }
}