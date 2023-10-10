using Godot;
using System;

public partial class Log : InteractablesBase
{
        public override void _Ready()
        {
                base._Ready();
                SetProcess(false);
                InteractionText.Hide();
        }

        public void OnBodyEntered(Node2D body)
        {
                PlayerEntered(body);
        }

        public void OnBodyExited(Node2D body)
        {
                PlayerExited(body);
        }

        protected override void SetTextLabel()
        {
                InteractionText.Text = Price + " To remove obstacle";
        }

        protected override void Interact()
        {
                if (PlayerGlobals.CanPay(Price))
                {
                        PlayerGlobals.SpendPoints(Price);
                        QueueFree();
                }
                else
                {
                        InteractionText.Text = "Can't afford that";
                }
        }
}
