using System;

namespace MyABP.Configuration
{
    /// <summary>
    /// 提供基于Dictionary来管理Configuration的接口
    /// </summary>
    public interface IDictionaryBasedConfig
    {
        /// <summary>
        /// 设置一个新的配置
        /// </summary>
        /// <typeparam name="T">配置值类型</typeparam>
        /// <param name="name">唯一配置名称</param>
        /// <param name="value">配置的值</param>
        void Set<T>(string name, T value);

        /// <summary>
        /// 根据给定的配置名称获取配置
        /// </summary>
        /// <param name="name">唯一的配置名称</param>
        /// <returns>配置的值或NULL</returns>
        object Get(string name);

        /// <summary>
        /// 根据给定的配置名称获取配置的泛型版本
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="name">配置名称</param>
        /// <returns></returns>
        T Get<T>(string name);

        /// <summary>
        /// 根据给定的配置名称获取配置，若未找到，则返回给定的默认值
        /// </summary>
        /// <param name="name">配置名称</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>返回找到的对应配置或给定默认值</returns>
        object Get(string name, object defaultValue);

        /// <summary>
        /// 根据给定的配置名称获取配置的泛型版本，若未找到，则返回给定的默认值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="name">配置的名称</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>返回找到的对应配置或给定默认值</returns>
        T Get<T>(string name, T defaultValue);

        /// <summary>
        /// 根据给定的配置名称获取配置，若不存在则调用对应的Func创建配置
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="name">配置名称</param>
        /// <param name="creator">创建配置的Func委托</param>
        /// <returns>返回找到的对应配置或NULL</returns>
        T GetOrCreate<T>(string name, Func<T> creator);
    }
}