using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyABP.Dependency
{
    /// <summary>
    /// 用来传递配置选项是否自动注入所有的实现当以常规的方式进行注入的时候
    /// </summary>
    public class ConventionalRegistrationConfig
    {
        /// <summary>
        /// 是否自动注入所有的实现<see cref="IInterCeptor"/>
        /// 默认：true
        /// </summary>
        public bool InstallInstallers { get; set; }

        /// <summary>
        /// 构造方法
        /// </summary>
        public ConventionalRegistrationConfig()
        {
            InstallInstallers = true;
        }
    }
}
