using Starcounter;

namespace KitchenSink {
    partial class ButtonPage : Json
    {
        void Handle(Input.AddCarrots action) {
            if (action.Value == 0) {
                CarrotsReaction = Template.CarrotsReaction.DefaultValue;
            }
            else {
                CarrotsReaction = "You have " + action.Value + " imaginary carrots";
            }
        }

        void Handle(Input.EnableCarrotEngine action) {
            if (action.Value == false) {
                CarrotEngineReaction = Template.CarrotEngineReaction.DefaultValue;
            }
            else {
                CarrotEngineReaction = "Carrot engine is on";
            }
        }

        void Handle(Input.AddOneCarrot action) {
            if (action.Value == 0) {
                OneCarrotReaction = Template.OneCarrotReaction.DefaultValue;
            }
            else {
                OneCarrotReaction = "You have " + action.Value + " imaginary carrots";
                AddOneCarrotDisabled = true;
            }
        }
    }
}
