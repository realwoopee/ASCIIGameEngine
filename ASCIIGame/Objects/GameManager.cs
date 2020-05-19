﻿using System;
using System.Collections.Generic;
using System.Text;

using ASCIIEngine.Core;
using ASCIIEngine.Core.BasicClasses;

namespace ASCIIGame.Objects
{
    public class GameManager : GameObject
    {
        public override bool HasCollider => false;
        public override string Tag => "gameManager";

        public Action ResetBonus;

        private PlayerHead player;

        public int Score { get; private set; }

        public GameManager(Action resetBonus, Vector2D worldSize)
        {
            ResetBonus = resetBonus;
        }

        public override void Start()
        {
            player = GameObjectPoolSingleton.Instance.GetObjectById("player") as PlayerHead;
        }

        public override void Step()
        {
            base.Step();
        }

        public void OnBonus()
        {
            Logger.PrintLine("Player ate a bonus.");
            Score++;
            player.AddTail();
            ResetBonus();
        }

        public void OnEnemy()
        {
            Logger.PrintLine("GIMME UR POITNZ.");
            Score = 0;
        }
    }
}
