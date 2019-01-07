using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace MultiLanguage
{
    [Serializable]
    public class LanguageBinding
    {
        /// <summary>
        /// 绑定名称,当名称为GLOBAL时，默认为全局配置
        /// <para>绑定名称:用于查询或过滤指定语言。</para>
        /// </summary>
        public string BindingName { get; set; }

        /// <summary>
        /// 多语言映射
        /// </summary>
        public List<LanguageMapper> Mappers { get; set; }
    }

    [Serializable]
    public class LanguageMapper
    {
        public string Target { get; set; }

        public Dictionary<string, string> Languages { get; set; }
    }

    public enum Languages
    {
        [Description("zh_CN")]
        ZH_CN,
        [Description("EN")]
        EN,
        [Description("JP")]
        JP
    }
}
