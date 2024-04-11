using System;
using GMToolKit.Inspector.UndoSystem;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace GMToolKit.Inspector
{
    [Drawer(typeof(Color))]
    internal class ColorDrawer : Drawer
    {
        ColorField view;
        Color previousValue;
        public override VisualElement Initialize()
        {
            view = new ColorField(this.Entry.memberInfo.Name);
            previousValue = (Color)this.Entry.Value;

            view.RegisterValueChangedCallback(e =>
            {
                var prevous = previousValue;
                var newValue = e.newValue;

                this.Entry.Value = newValue;
                var undoCommand = new UndoCommand()
                {
                    Do = () => { this.Entry.Value = newValue; },
                    Undo = () => { this.Entry.Value = prevous; }
                };
                UndoSystem.UndoSystem.Record(undoCommand);

                previousValue = newValue;
            });

            return view;
        }

        public override void Tick()
        {
            var sourceValue = (Color)this.Entry.Value;
            var textValue = this.view.value;

            if (sourceValue != textValue)
            {
                this.view.SetValueWithoutNotify(sourceValue);
            }
        }
    }

    [Drawer(typeof(Color32))]
    internal class Color32Drawer : Drawer
    {
        ColorField view;
        Color32 previousValue;
        public override VisualElement Initialize()
        {
            view = new ColorField(this.Entry.memberInfo.Name);
            previousValue = (Color32)this.Entry.Value;

            view.RegisterValueChangedCallback(e =>
            {
                Color32 prevous = previousValue;
                Color32 newValue = e.newValue;

                this.Entry.Value = newValue;
                var undoCommand = new UndoCommand()
                {
                    Do = () => { this.Entry.Value = newValue; },
                    Undo = () => { this.Entry.Value = prevous; }
                };
                UndoSystem.UndoSystem.Record(undoCommand);

                previousValue = newValue;
            });

            return view;
        }

        public override void Tick()
        {
            var sourceValue = (Color32)this.Entry.Value;
            var textValue = this.view.value;

            if (sourceValue != textValue)
            {
                this.view.SetValueWithoutNotify(sourceValue);
            }
        }
    }
}