using Godot;
using System;
using System.IO;

public partial class Log : InteractablesBase
{
        [Export]
        private NavigationRegion2D PathOpened{get;set;}
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
                        if (!Area.IsOpen)
                        {
                                Area.IsOpen = true;
                                Area.IsActive = true;
                        }
                        PlayerGlobals.SpendPoints(Price);
                        PathOpened.Enabled = true;
                        QueueFree();
                }
                else
                {
                        InteractionText.Text = "Can't afford that";
                }
        }
}
