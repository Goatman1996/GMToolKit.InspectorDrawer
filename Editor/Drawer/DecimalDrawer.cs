using System;
using GMToolKit.Inspector.UndoSystem;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace GMToolKit.Inspector
{
    [Drawer(typeof(decimal))]
    internal class DecimalDrawer : Drawer
    {
        DecimalNumberField view;
        public override VisualElement Initialize()
        {
            view = new DecimalNumberField(this.Entry.memberInfo.Name);

            view.RegisterValueChangedCallback(e =>
            {
                this.Entry.Value = view.value;
            });

            view.SetEnabled(this.Entry.IsSettable());

            view.OnEdited += this.OnEdited;

            return view;
        }

        private void OnEdited(decimal previous, decimal newValue)
        {
            var undoCommand = new UndoCommand()
            {
                Do = () => { this.Entry.Value = newValue; },
                Undo = () => { this.Entry.Value = previous; }
            };
            UndoSystem.UndoSystem.Record(undoCommand);
        }

        public override void Tick()
        {
            var sourceValue = (decimal)this.Entry.Value;
            var textValue = this.view.value;
            if (sourceValue != textValue)
            {
                this.view.SetValueWithoutNotify(sourceValue);
            }
        }
    }
}