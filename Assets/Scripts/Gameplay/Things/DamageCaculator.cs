﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class DamageCaculator {
    //TODO:伤害在这里计算，每个单位可能有不同的计算伤害的配置
    public class DamageResult
    {

    }

    
}

public class DamageDefine
{
    //TODO:伤害武器的来源：
    //近战：钝器，利器，锐器
    ////钝器:锤子，棒子 （钝伤）
    ////利器:刀，斧，剑 （利器伤害）
    ////锐器:枪 （穿刺伤害）
    //远程:枪械
    ////击穿护甲前造成（钝器伤害） 击穿护甲后造成（穿刺伤害）
    //枪械主要造成的伤害取决于子弹,子弹有口径+弹药种类（不同弹头 不同装药）+ 子弹速度的区别
    //不同种的枪械作为子弹的发射器存在，影响装弹速度，瞄准速度，子弹初速，子弹偏移（首发精度和后续射击的扩散），射速，开火模式，本身没有对伤害有影响
    //通过不同的配件可以修改同种枪支的不同属性，例如降低后座（子弹偏移），提升瞄准速度（人机工效），弹匣容量，子弹初速（伤害提升）
    //某些特殊配件有其他功效，例如战术手电（瞄准时照亮，提高黑暗空间中的命中率） 霰弹枪收束器（减小弹丸扩散）等

    //TODO:受击者的属性
    //不同部位有不同血量，和Rimworld一样，打烂了就会变成Debuff，考虑像塔科夫一样引入骨折，流血（和Rimworld的流血不同的是，在提高全身失血量的同时还会减少该部位的血量，直到该部位彻底损坏）
    //不同的部位被命中的概率，倒是可以通过子弹弹丸接触到单位的位置与单位面朝方向乘积计算出在左边还是右边，然后越靠中间的位置越容易击中躯干，角度越大越容易击中左/右腿和手臂
    //部位的分布为一颗树，例如头->躯干->手臂->手 不同种类的生物可能有不同的部位分布，例如俩只手在地上爬行的丧尸，就没有腿的存在，但是基础速度会变慢，并且手臂会影响移动效率之类的设定

}