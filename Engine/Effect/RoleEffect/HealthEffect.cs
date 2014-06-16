﻿using Engine.Client;
using Engine.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Engine.Effect
{
    /// <summary>
    /// 治疗效果
    /// </summary>
    public class HealthEffect : EffectDefine, IEffectHandler
    {
        /// <summary>
        /// 生命值回复表达式
        /// </summary>
        public String 生命值回复表达式 = String.Empty;
        /// <summary>
        /// 护甲回复表达式
        /// </summary>
        public String 护甲回复表达式 = String.Empty;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="game"></param>
        /// <param name="singleEffect"></param>
        /// <param name="MeOrYou"></param>
        void IEffectHandler.DealHero(GameManager game, EffectDefine singleEffect, bool MeOrYou)
        {
            int ShieldPoint = ExpressHandler.GetEffectPoint(game, 护甲回复表达式);
            int HealthPoint = ExpressHandler.GetEffectPoint(game, 生命值回复表达式);
            if (MeOrYou)
            {
                game.MyInfo.AfterBeShield(ShieldPoint);
                if (game.MyInfo.AfterBeHealth(HealthPoint))
                {
                    game.事件处理组件.事件池.Add(new Engine.Utility.CardUtility.全局事件()
                    {
                        事件类型 = CardUtility.事件类型列表.治疗,
                        触发方向 = CardUtility.TargetSelectDirectEnum.本方,
                        触发位置 = Engine.Client.BattleFieldInfo.HeroPos
                    });
                }
            }
            else
            {
                game.YourInfo.AfterBeShield(ShieldPoint);
                if (game.YourInfo.AfterBeHealth(HealthPoint))
                {
                    game.事件处理组件.事件池.Add(new Engine.Utility.CardUtility.全局事件()
                    {
                        事件类型 = CardUtility.事件类型列表.治疗,
                        触发方向 = CardUtility.TargetSelectDirectEnum.对方,
                        触发位置 = Engine.Client.BattleFieldInfo.HeroPos
                    });
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="game"></param>
        /// <param name="singleEffect"></param>
        /// <param name="MeOrYou"></param>
        /// <param name="PosIndex"></param>
        void IEffectHandler.DealMinion(GameManager game, EffectDefine singleEffect, bool MeOrYou, int PosIndex)
        {
            int HealthPoint = ExpressHandler.GetEffectPoint(game, 生命值回复表达式);
            if (MeOrYou)
            {
                if (game.MyInfo.BattleField.BattleMinions[PosIndex].AfterBeHealth(HealthPoint))
                {
                    game.事件处理组件.事件池.Add(new Engine.Utility.CardUtility.全局事件()
                    {
                        事件类型 = CardUtility.事件类型列表.治疗,
                        触发方向 = CardUtility.TargetSelectDirectEnum.本方,
                        触发位置 = PosIndex + 1
                    });
                }
            }
            else
            {
                if (game.YourInfo.BattleField.BattleMinions[PosIndex].AfterBeHealth(HealthPoint))
                {
                    game.事件处理组件.事件池.Add(new Engine.Utility.CardUtility.全局事件()
                    {
                        事件类型 = CardUtility.事件类型列表.治疗,
                        触发方向 = CardUtility.TargetSelectDirectEnum.对方,
                        触发位置 = PosIndex + 1
                    });
                }
            }
        }
    }
}
