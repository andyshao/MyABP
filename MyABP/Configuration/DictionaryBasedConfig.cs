using System;
using System.Collections.Generic;

namespace MyABP.Configuration
{
    /// <summary>
    /// 用于获取或设置配置
    /// </summary>
    public class DictionaryBasedConfig : IDictionaryBasedConfig
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="customSettings"></param>
        public DictionaryBasedConfig(Dictionary<string, object> customSettings)
        {
            CustomSettings = customSettings;
        }

        /// <summary>
        /// 自定义配置字典
        /// </summary>
        protected Dictionary<string, object> CustomSettings { get; private set; }

        /// <summary>
        /// 获取或设置配置
        /// </summary>
        /// <param name="name">配置名称</param>
        /// <returns>配置的值或Null</returns>
        public object this[string name]
        {
            get
            {
                object result = null;
                CustomSettings.TryGetValue(name, out result);
                return result;
            }
            set { CustomSettings[name] = value; }
        }

        /// <summary>
        /// 设置配置
        /// </summary>
        /// <typeparam name="T">配置的值类型</typeparam>
        /// <param name="name">配置名称</param>
        /// <param name="value">配置值</param>
        public void Set<T>(string name, T value)
        {
            this[name] = value;
        }

        /// <summary>
        /// 获取配置的值
        /// </summary>
        /// <param name="name">配置名称</param>
        /// <returns>配置值</returns>
        public object Get(string name)
        {
            return Get(name, null);
        }

        /// <summary>
        /// 获取配置的值
        /// </summary>
        /// <typeparam name="T">配置的值类型</typeparam>
        /// <param name="name">配置名称</param>
        /// <returns>配置值</returns>
        public T Get<T>(string name)
        {
            var result = this[name];
            return result == null ? default(T) : (T)Convert.ChangeType(result, typeof(T));
        }

        /// <summary>
        /// 获取配置的值，若为空，返回指定的默认值
        /// </summary>
        /// <param name="name">配置名称</param>
        /// <param name="defaultValue"></param>
        /// <returns>配置值</returns>
        public object Get(string name, object defaultValue)
        {
            var result = this[name];
            return result ?? defaultValue;
        }

        /// <summary>
        /// 获取配置的值，若为空，返回指定的默认值
        /// </summary>
        /// <typeparam name="T">配置的值类型</typeparam>
        /// <param name="name">配置名称</param>
        /// <param name="defaultValue"></param>
        /// <returns>配置值</returns>
        public T Get<T>(string name, T defaultValue)
        {
            return (T) Get(name, (object) defaultValue);
        }

        /// <summary>
        /// 获取或设置配置
        /// </summary>
        /// <typeparam name="T">配置的值类型</typeparam>
        /// <param name="name">配置名称</param>
        /// <param name="creator">创建配置的委托</param>
        /// <returns>配置值</returns>
        public T GetOrCreate<T>(string name, Func<T> creator)
        {
            var result = this[name];
            if (result == null)
            {
                result = creator();
                Set(name,result);
            }
            return (T) result;
        }
    }
}