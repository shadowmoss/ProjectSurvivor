/****************************************************************************
 * Copyright (c) 2015 ~ 2022 liangxiegame MIT License
 *
 * QFramework v1.0
 *
 * https://qframework.cn
 * https://github.com/liangxiegame/QFramework
 * 
 * Author:
 *  liangxie
 *  王二 soso
 * Contributor
 *  TastSong
 * 
 * Community
 *  QQ Group: 623597263
 ****************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
namespace QFramework
{
    #region Architecture
    public interface IArchitecture
    {
        #region 系统模块相关接口
        // 注册系统层API
        void RegisterSystem<T>(T instance) where T : ISystem;
        // 提供一个获取ISystem的API
        T GetSystem<T>() where T : class, ISystem;
        #endregion
        #region 模型对象相关接口
        // 提供一个注册IModel的API
        void RegisterModel<T>(T instance) where T : IModel;
        // 提供一个可以获取IModel的APi
        T GetModel<T>() where T : class, IModel;
        #endregion
        #region 工具相关接口
        // 提供一个注册Utility的API
        void RegisterUtility<T>(T instance);

        // 提供一个获取Utility的API
        T GetUtility<T>() where T : class;
        #endregion
        #region 命令相关接口
        // 发送指令
        void SendCommand<T>() where T : ICommand, new();

        // 发送指令
        void SendCommand<T>(T command) where T : ICommand;
        #endregion
        #region 事件接口
        // 有关事件的接口
        // 注册
        IUnRegister RegisterEvent<T>(Action<T> onEvent);
        // 注销
        void UnRegisterEvent<T>(Action<T> onEvent);
        // 根据事件类型发送事件
        void SendEvent<T>() where T : new();
        // 用户自定义事件类型发送事件
        void SendEvent<T>(T e);
        #endregion
        #region 查询相关接口
        TResult SendQuery<TResult>(IQuery<TResult> query);
        #endregion
    }
    public abstract class Architecture<T> : IArchitecture where T : Architecture<T>, new()
    {
        // 用于切换注册的模块的委托,这里的T代表框架对象本身,也就是一个需要传入Architecutre类型返回值为void的委托
        public static Action<T> OnRegisterPatch = architecture => { };
        // 用于确认当前框架是否初始化完成
        private bool mInited = false;

        // 用于初始化IModel的缓存队列
        List<IModel> mModels = new List<IModel>();

        // 用于初始化ISystem的缓存队列
        List<ISystem> mSystems = new List<ISystem>();

        #region 类似单例模式 但是仅在内部访问使用
        private static T mArchitecture = null;

        // 确保Container是有实例的
        static void MakeSureArchitecture()
        {
            if (mArchitecture == null)
            {
                mArchitecture = new T();
                // 这里只执行注册，不执行各个模块的完全初始化。
                mArchitecture.Init();

                OnRegisterPatch?.Invoke(mArchitecture);

                // 初始化IModel
                foreach (IModel architectureModel in mArchitecture.mModels)
                {
                    architectureModel.Init();
                }

                // 清空缓存的IModel队列
                mArchitecture.mModels.Clear();

                // 初始化ISystem
                foreach (ISystem architectureSystem in mArchitecture.mSystems)
                {
                    architectureSystem.Init();
                }
                mArchitecture.mSystems.Clear();
                // 修改标志位表示初始化完成
                mArchitecture.mInited = true;
            }
        }
        #endregion

        public static IArchitecture Instance
        {
            get
            {
                if (mArchitecture == null)
                {
                    MakeSureArchitecture();
                }
                return mArchitecture;
            }
        }

        private IOCContainer mContainer = new IOCContainer();

        // 留给子类注册模块
        protected abstract void Init();
        #region 静态的注册获取API
        // 提供一个注册模块的API,现在是静态的
        public static void Register<TStatic>(TStatic instance)
        {
            MakeSureArchitecture();
            mArchitecture.mContainer.Register<TStatic>(instance);
        }
        #endregion

        #region 非静态的API
        #region Model模块相关接口
        // 提供一个注册Model的API
        public void RegisterModel<TModel>(TModel instance) where TModel : IModel
        {
            // 给每个注册进框架的Model持有当前框架对象
            instance.SetArchitecture(mArchitecture);
            // 将Model注册进框架
            mContainer.Register<TModel>(instance);

            // 如果初始化过了
            if (mInited)
            {
                instance.Init();
            }
            else
            {
                // 添加到Model缓存中,用于初始化
                mModels.Add(instance);
            }
        }
        // 提供一个获取IModel层的方法
        public TModel GetModel<TModel>() where TModel : class, IModel
        {
            return mContainer.Get<TModel>();
        }
        #endregion
        #region 工具模块相关接口
        // 注册工具模块的API
        public void RegisterUtility<TUtility>(TUtility instance)
        {
            mContainer.Register<TUtility>(instance);
        }
        // 获取工具模块的API
        public TUtility GetUtility<TUtility>() where TUtility : class
        {
            return mContainer.Get<TUtility>();
        }
        #endregion
        #region 系统模块相关接口
        // 注册系统层API
        public void RegisterSystem<TSystem>(TSystem instance) where TSystem : ISystem
        {
            // System层和Model层差不多，有可能会获取Model或者Utility层.所以需要持有architecture对象防止递归调用
            instance.SetArchitecture(this);

            mContainer.Register<TSystem>(instance);

            // 如果当前框架初始化过了
            if (mInited)
            {
                instance.Init();
            }
            else
            {
                // 将当前System添加到缓存列表当中
                mSystems.Add(instance);
            }
        }
        // 提供一个获取ISystem的API
        public TSystem GetSystem<TSystem>() where TSystem : class, ISystem
        {
            return mContainer.Get<TSystem>();
        }
        #endregion
        #region 命令模块相关接口
        public void SendCommand<TCommand>() where TCommand : ICommand, new()
        {
            TCommand command = new TCommand();
            command.SetArchitecture(this);
            command.Execute();
        }

        public void SendCommand<TCommand>(TCommand command) where TCommand : ICommand
        {
            command.SetArchitecture(this);
            command.Execute();
        }
        #endregion
        #region 事件模块相关接口

        private ITypeEventSystem mTypeEventSystem = new TypeEventSystem();
        public IUnRegister RegisterEvent<TEvent>(Action<TEvent> onEvent)
        {
            return mTypeEventSystem.Register<TEvent>(onEvent);
        }

        public void UnRegisterEvent<TEvent>(Action<TEvent> onEvent)
        {
            mTypeEventSystem.UnRegister<TEvent>(onEvent);
        }

        public void SendEvent<TEvent>() where TEvent : new()
        {
            mTypeEventSystem.Send<TEvent>();
        }

        public void SendEvent<TEvent>(TEvent e)
        {
            mTypeEventSystem.Send<TEvent>(e);
        }
        #endregion
        #region 查询相关接口
        public TResult SendQuery<TResult>(IQuery<TResult> query)
        {
            query.SetArchitecture(this);
            return query.Do();
        }
        #endregion
        #endregion
    }
    #endregion

    #region Controller
    public interface IController : IBelongToArchitecture, ICanGetSystem, ICanGetModel, ICanSendCommand, ICanRegisterEvent, ICanSendQuery
    {
    }
    #endregion

    #region System
    // 系统层是用来做一些可能临时存储状态相关的操作。
    // 与各层之间的通信关系
    // 表现层通过Command与System层和Model层进行通信
    // System层与Model层通过委托或者事件通知表现层
    // 表现层不与Utility层直接通信
    // 表现层可通过直接获取System层和Model层对象，获取相应的值状态
    // 
    public interface ISystem : IBelongToArchitecture, ICanSetArchitecture, ICanGetUtility, ICanGetModel, ICanSendEvent, ICanRegisterEvent, ICanGetSystem
    {
        void Init();
    }

    public abstract class AbstractSystem : ISystem
    {
        private IArchitecture mArchitecture = null;
        IArchitecture IBelongToArchitecture.GetArchitecture()
        {
            return mArchitecture;
        }
        void ICanSetArchitecture.SetArchitecture(IArchitecture architecture)
        {
            mArchitecture = architecture;
        }
        void ISystem.Init()
        {
            OnInit();
        }
        protected abstract void OnInit();
    }
    #endregion

    #region Model
    // 为了解决Model模块与Architecture之间的初始化无限递归问题，
    // 决定将Model和Architecture之间互相持有各自的对象。
    // Architecture通过注册方法持有Model对象
    // Model通过IBelongToArchitecture的IArchitecture属性持有Architecture对象
    // 但是Model的构造器方法获取Arhictecture对象的时间点早于，将Model注册进Architecture，此时Model构造其中获取Architecture获取不到，
    // 因为将Arhictecture对象交给Model持有是在Model对象的构造器方法调用之后。
    // 解决方法:
    // 将Model对象的完全初始化延后，先将Model对象注册进Architecture当中,并让它持有Architecture对象。
    public interface IModel : IBelongToArchitecture, ICanSetArchitecture, ICanGetUtility, ICanSendEvent
    {
        // Model类型的初始化生命周期方法，在这里进行完全的初始化。
        void Init();
    }
    public abstract class AbstractModel : IModel
    {
        private IArchitecture mArchitecture = null;
        IArchitecture IBelongToArchitecture.GetArchitecture()
        {
            return mArchitecture;
        }
        void ICanSetArchitecture.SetArchitecture(IArchitecture architecture)
        {
            mArchitecture = architecture;
        }
        void IModel.Init()
        {
            OnInit();
        }
        protected abstract void OnInit();
    }
    #endregion

    #region Utility
    public interface IUtility
    {

    }
    #endregion

    #region Command
    public interface ICommand : IBelongToArchitecture, ICanSetArchitecture, ICanGetSystem, ICanGetModel, ICanGetUtility, ICanSendEvent, ICanSendCommand, ICanSendQuery
    {
        void Execute();
    }

    public abstract class AbstractCommand : ICommand
    {
        private IArchitecture mArchitecture;
        IArchitecture IBelongToArchitecture.GetArchitecture()
        {
            return mArchitecture;
        }
        void ICanSetArchitecture.SetArchitecture(IArchitecture architecture)
        {
            mArchitecture = architecture;
        }
        void ICommand.Execute()
        {
            OnExecute();
        }
        protected abstract void OnExecute();
    }
    #endregion

    #region Query
    public interface IQuery<TResult> : IBelongToArchitecture, ICanSetArchitecture, ICanGetModel, ICanGetSystem, ICanGetUtility
    {
        TResult Do();
    }
    public abstract class AbstractQuery<T> : IQuery<T>
    {

        private IArchitecture mArchitecture;
        public T Do()
        {
            return OnDo();
        }

        protected abstract T OnDo();

        IArchitecture IBelongToArchitecture.GetArchitecture()
        {
            return mArchitecture;
        }

        void ICanSetArchitecture.SetArchitecture(IArchitecture architecture)
        {
            mArchitecture = architecture;
        }
    }
    #endregion

    #region Rule
    public interface IBelongToArchitecture
    {
        IArchitecture GetArchitecture();
    }
    public interface ICanSetArchitecture
    {
        void SetArchitecture(IArchitecture architecture);
    }
    public interface ICanGetModel : IBelongToArchitecture
    {

    }
    public static class CanGetModelExtension
    {
        public static T GetModel<T>(this ICanGetModel self) where T : class, IModel
        {
            return self.GetArchitecture().GetModel<T>();
        }
    }
    public interface ICanGetSystem : IBelongToArchitecture
    {

    }
    public static class CanGetSystemExtension
    {
        public static T GetSystem<T>(this ICanGetSystem self) where T : class, ISystem
        {
            return self.GetArchitecture().GetSystem<T>();
        }
    }
    public interface ICanGetUtility : IBelongToArchitecture
    {

    }
    public static class CanGetUtilityExtension
    {
        public static T GetUtility<T>(this ICanGetUtility self) where T : class, IUtility
        {
            return self.GetArchitecture().GetUtility<T>();
        }
    }
    public interface ICanRegisterEvent : IBelongToArchitecture
    {

    }
    public static class CanRegisterEventExtension
    {
        public static IUnRegister RegisterEvent<T>(this ICanRegisterEvent self, Action<T> onEvent)
        {
            return self.GetArchitecture().RegisterEvent<T>(onEvent);
        }
        public static void UnRegisterEvent<T>(this ICanRegisterEvent self, Action<T> onEvent)
        {
            self.GetArchitecture().UnRegisterEvent<T>(onEvent);
        }
    }
    public interface ICanSendCommand : IBelongToArchitecture
    {

    }
    public static class CanSendCommandExtension
    {
        public static void SendCommand<T>(this ICanSendCommand self) where T : ICommand, new()
        {
            self.GetArchitecture().SendCommand<T>();
        }
        public static void SendCommand<T>(this ICanSendCommand self, T command) where T : ICommand
        {
            self.GetArchitecture().SendCommand(command);
        }
    }
    public interface ICanSendEvent : IBelongToArchitecture
    {

    }
    public static class CanSendEventExtension
    {
        public static void SendEvent<T>(this ICanSendEvent self) where T : new()
        {
            self.GetArchitecture().SendEvent<T>();
        }
        public static void SendEvent<T>(this ICanSendEvent self, T eventData)
        {
            self.GetArchitecture().SendEvent<T>(eventData);
        }
    }
    public interface ICanSendQuery : IBelongToArchitecture
    {

    }
    public static class CanSendQueryExtension
    {
        public static T SendQuery<T>(this ICanSendQuery self, IQuery<T> query)
        {
            return self.GetArchitecture().SendQuery(query);
        }
    }
    #endregion

    #region TypeEventSystem
    // 事件管理系统的操作接口
    public interface ITypeEventSystem
    {
        void Send<T>() where T : new();
        void Send<T>(T e);
        IUnRegister Register<T>(Action<T> onEvent);
        void UnRegister<T>(Action<T> onEvent);
    }

    // 事件管理类
    public class TypeEventSystem : ITypeEventSystem
    {
        public static readonly TypeEventSystem Global = new TypeEventSystem();

        private Dictionary<Type, IRegistrations> mEventRegistrations = new Dictionary<Type, IRegistrations>();
        // 事件监听注册方法API
        // T类型为监听的对应的事件类型,监听方法接受事件对象进行处理
        public IUnRegister Register<T>(Action<T> onEvent)
        {
            Type type = typeof(T);
            IRegistrations eventRegistrations;
            // 查找当前字典集合当中是否存在,目标监听事件

            if (mEventRegistrations.TryGetValue(type, out eventRegistrations))
            {
                // 获取到了

            }
            else
            {
                // 获取不到 新建一个监听接受对象
                eventRegistrations = new Registrations<T>();
                // 将目标事件类型和监听接收对象连接起来
                mEventRegistrations.Add(type, eventRegistrations);
            }
            // 将监听方法引用传给监听接收对象
            (eventRegistrations as Registrations<T>).OnEvent += onEvent;

            // 对应返回一个用于注销监听的对象,IUnRegister类型的扩展方法中已包含对应的注销操作UnRegisterWhenGameObjectDestroyed()方法
            return new TypeEventSystemUnRegister<T>()
            {
                OnEvent = onEvent,
                TypeEventSystem = this
            };
        }
        public void UnRegister<T>(Action<T> onEvent)
        {
            Type type = typeof(T);
            IRegistrations eventRegistrations;
            if (mEventRegistrations.TryGetValue(type, out eventRegistrations))
            {
                // 获取到了待注销监听方法的目标方法，并取出了对应接受监听委托的对象。
                (eventRegistrations as Registrations<T>).OnEvent -= onEvent;
            }
        }
        // 直接通过类型发送事件API
        public void Send<T>() where T : new()
        {
            T triggeredEvent = new T();
            Send<T>(triggeredEvent);
        }
        // 触发事件的方法T类型标识对应的事件类型，事件类型对象当中存放需要传递给监听方法的处理参数
        // 用户自建事件对象进行事件触发。
        public void Send<T>(T e)
        {
            Type type = typeof(T);
            IRegistrations eventRegistrations;

            if (mEventRegistrations.TryGetValue(type, out eventRegistrations))
            {
                (eventRegistrations as Registrations<T>)?.OnEvent.Invoke(e);
            }
        }


    }
    // 新增的通过接口方式进行事件监听
    public interface IOnEvent<T> { 
        void OnEvent(T e);
    }
    public static class OnGlobalEventExtension {
        public static IUnRegister RegisterEvent<T>(this IOnEvent<T> self) where T : struct {
            return TypeEventSystem.Global.Register<T>(self.OnEvent);
        }
        public static void UnRegisterEvent<T>(this IOnEvent<T> self) where T : struct
        {
            TypeEventSystem.Global.UnRegister<T>(self.OnEvent);
        }
    }
    // 标记注册的监听事件的委托
    public interface IRegistrations
    {

    }

    public class Registrations<T> : IRegistrations
    {
        // 接受待注册用于监听事件的委托
        public Action<T> OnEvent = obj => { };
    }
    // 监听注销类操作接口
    public interface IUnRegister
    {
        void UnRegister();
    }

    public static class UnRegisterExtension
    {
        public static void UnRegisterWhenGameObjectDestroyed(this IUnRegister unRegister, GameObject gameObject)
        {
            var trigger = gameObject.GetComponent<UnRegisterOnDestroyTrigger>();
            // 如果该GameObject不存在监听注销器组件。
            // 为其添加一个
            if (!trigger)
            {
                trigger = gameObject.AddComponent<UnRegisterOnDestroyTrigger>();
            }
            // 将当前监听注销对象放入监听注销器组件的HashSet集合当中
            trigger.AddUnRegister(unRegister);
        }
    }

    // 监听注销实现类
    public class TypeEventSystemUnRegister<T> : IUnRegister
    {
        // 持有事件管理类
        public ITypeEventSystem TypeEventSystem { get; set; }
        // 监听操作委托对象,当事件触发了需要做什么操作
        public Action<T> OnEvent { get; set; }
        // 监听注销方法
        public void UnRegister()
        {
            TypeEventSystem.UnRegister(OnEvent);
            TypeEventSystem = null;
            OnEvent = null;
        }
    }

    public class UnRegisterOnDestroyTrigger : MonoBehaviour
    {
        private HashSet<IUnRegister> mUnRegister = new HashSet<IUnRegister>();

        public void AddUnRegister(IUnRegister unRegister)
        {
            mUnRegister.Add(unRegister);
        }

        private void OnDestroy()
        {
            foreach (var unRegister in mUnRegister)
            {
                unRegister.UnRegister();
            }
            mUnRegister.Clear();
        }
    }
    #endregion

    #region IOC
    public class IOCContainer
    {
        // IOC容器，一个以Type为键，对应的object实例为值的字典集合
        public Dictionary<Type, object> mInstances = new Dictionary<Type, object>();

        // 将对象注册到IOC容器
        public void Register<T>(T instance)
        {
            var key = typeof(T);

            if (mInstances.ContainsKey(key))
            {
                mInstances[key] = instance;
            }
            else
            {
                mInstances.Add(key, instance);
            }
        }

        // 从IOC容器中获取对象。
        public T Get<T>() where T : class
        {
            var key = typeof(T);
            object retObj;
            if (mInstances.TryGetValue(key, out retObj))
            {
                return retObj as T;
            }
            return null;
        }
    }
    #endregion

    #region BindableProperty
    // 一个用于数据绑定的专用泛型类.
    // 数据+数据变更事件的集合体。
    public class BindableProperty<T>
    {
        public BindableProperty(T defaultValue = default) {
            mValue = defaultValue;
        }
        private T mValue;

        // 在数据变化时,调用外部委托代码。
        public T Value
        {
            get => mValue;
            set
            {
                if (value == null && mValue == null) return;
                if (value != null && value.Equals(mValue)) return;
                    mValue = value;
                    mOnValueChanged?.Invoke(value);
            }
        }

        private Action<T> mOnValueChanged=(v)=> { };

        public IUnRegister Register(Action<T> onValueChanged)
        {
            mOnValueChanged += onValueChanged;
            return new BindablePropertyUnRegister<T>()
            {
                BindableProperty = this,
                OnValueChanged = onValueChanged
            };
        }
        // 注册时根据初始值触发一次。
        public IUnRegister RegisterWithInitValue(Action<T> onValueChanged) { 
            onValueChanged(mValue);
            return Register(onValueChanged);
        }

        public static implicit operator T(BindableProperty<T> property) {
            return property.Value;
        }

        public void UnRegister(Action<T> onValueChanged)
        {
            mOnValueChanged -= onValueChanged;
        }
        public override string ToString()
        {
            return Value.ToString();
        }
    }

    public class BindablePropertyUnRegister<T> : IUnRegister
    {
        public BindableProperty<T> BindableProperty { get; set; }

        public Action<T> OnValueChanged { get; set; }

        public void UnRegister()
        {
            BindableProperty.UnRegister(OnValueChanged);
            BindableProperty = null;
            OnValueChanged = null;
        }
    }
    #endregion
}
