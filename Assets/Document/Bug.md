# 2026/05/27 目前已知的Bug
Game场景加载之时，会导致当前场景中的Enemy调用其OnDestroy方法，从而导致敌人数量减到负数，这个目前还不知道该怎么解决。
* Solution调用初始化数据方法时，重置全局数据在重新加载场景之前。目前的解决办法是先不要在全局重置方法里重置敌人数量的变量。因为这个变量在敌人销毁之时会自动调用数量减少方法。
# 2026/05/28 Game通关的判断条件问题
1. 在游戏首次通关时，按下空格键重启游戏时，似乎是已经达到触发GamePassPanel开启的条件，导致两次出现GamePass界面
* 在GamePanel中检查GamePassPanel开启的条件
* Bug出现原因，在当前场景重新加载时，实际上内存中的MonoBehaviour对象的成员变量的值并没有改变，导致已经判断达到敌人波次最后一波的判断条件始终生效。
* Solution在EnemyGenerator的OnDestroy方法中将WaveCount变量进行重置
2. 在游戏通关之后选择回到主界面，进行能力升级之后，并没有使得Coin的金币数量减少
* 在PoweUp按钮点击事件之后将数值进行一次Save
# 2026/06/04 金币升级面板在金币数量达到了，指定数量之后不显示
* 已修改
# 2026/06/05 金币升级之后，并没有存档之前已经升级过的内容了。这个和金币升级的事件监听精细化到了，单个选项有关。
# 2026/06/05 经验值达到之后，经验值升级面板的内容提示对象未指向一个对象。
* 已解决，主要问题在于，ExpUpdatePanel面板在GameScene界面处于disactive状态，导致.Show()时，会提示，GameObject不存在。
* 在ExpUpdatePanel中，创建具体的ExpUpdateItem项时，修改显示文字的需要到GetComponentInChild()中获取，结果我直接在Button组件当中进行获取Text，导致报错。
# 2026/06/15 敌人生成、波次系统ScriptableObject、敌人死亡动画特效
* 目前稍微困惑的点：Unity的可视化Shader系统。Unity Update FixedUpdate调用的差别，SceneManager特性，UGUI的层次结构，TileMap创建。
# 2026/06/17 Sprite的类型，以及Sprite2D的pivot在具体的代码中起到什么作用？
* QFramework ActionKit功能回顾。欧拉角变化。以及他这个幸存者游戏中，小刀动画步骤拆解。
# 2026/06/20 RotateSword，围绕Player的角度变化公式，是怎么得出的。以及Unity相关的坐标系学习。
* 目前来看想要自己完全独立实现ProjectSurvivor当中的图像变化公式，还得学习。