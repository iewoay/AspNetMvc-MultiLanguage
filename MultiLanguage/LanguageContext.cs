using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MultiLanguage
{

    public static class LanguageContext
    {

        private const string LANGUAGE_PACKS = "*LanguagePacks.dll";

        private static object locker = new object();
        /// <summary>
        /// 获取语言绑定信息
        /// </summary>
        /// <returns></returns>
        public static List<LanguageBinding> Bindings
        {
            get;
            private set;
        } = new List<LanguageBinding>();

        /// <summary>
        /// 获取全局绑定信息
        /// </summary>
        public static LanguageBinding Global
        {
            get
            {
                return Bindings.FirstOrDefault(p => !string.IsNullOrWhiteSpace(p.BindingName) && p.BindingName.ToUpper() == "GLOBAL");
            }
        }

        /// <summary>
        /// 是否有多语言包
        /// </summary>
        public static bool HasPack
        {
            get;
            private set;
        }

        /// <summary>
        /// 初始化多语言（在Global/Application_Start中调用）
        /// </summary>
        public static void Initialize()
        {
            Loader();
        }

        /// <summary>
        /// 根据绑定名称获取多语言映射信息
        /// </summary>
        /// <param name="bindingName"></param>
        /// <returns></returns>
        public static List<LanguageMapper> GetMappers(string bindingName)
        {
            if (string.IsNullOrWhiteSpace(bindingName))
                return Global.Mappers;

            var mappers = new List<LanguageMapper>();
            if (Bindings.Count > 0)
            {
                var find = Bindings.Where(p => !string.IsNullOrWhiteSpace(bindingName) && p.BindingName.ToLower() == bindingName.ToLower())
                    .DistinctBy(b => b.BindingName.ToLower());
                mappers = find.SelectMany(p => p.Mappers).ToList();
                if (Global != null)
                    mappers.AddRange(Global.Mappers);
            }
            return mappers;
        }

        /// <summary>
        /// 从指定绑定中获取指定目标语言内容
        /// </summary>
        /// <param name="target"></param>
        /// <param name="lang"></param>
        /// <param name="bindingName"></param>
        /// <returns></returns>
        public static string GetContent(string target, string lang, string bindingName)
        {
            var mapper = GetMappers(bindingName)?.FirstOrDefault(p => p.Target.ToLower() == target.ToLower());
            if (mapper == null)
                return target;
            lang = lang.ToLower();
            var languages = mapper.Languages;
            if (languages == null || languages.Count == 0)
                return target;
            var result = languages.ContainsKey(lang) ? mapper.Languages[lang] : target;
            return result == null ? target : result;
        }

        /// <summary>
        /// 从全局绑定中获取目标语言内容
        /// </summary>
        /// <param name="target"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        public static string GetContent(string target, string lang)
        {
            if (Global == null)
                return target;
            lang = lang.ToLower();
            var mapper = Global.Mappers.FirstOrDefault(p => p.Target.ToLower() == target.ToLower());
            var languages = mapper.Languages;
            if (languages == null || languages.Count == 0)
                return target;
            var result = mapper.Languages.ContainsKey(lang) ? mapper.Languages[lang] : target;
            return result == null ? target : result;
        }


        #region 私有方法 加载语言包
        /// <summary>
        /// 加载语言包
        /// </summary>
        private static void Loader()
        {
            try
            {
                var pack = GetPackFile();
                if (pack == null) return;
                var files = GetResources(pack);
                if (files.Count == 0) return;
                files.ForEach(content =>
                {
                    var binding = JsonConvert.DeserializeObject<LanguageBinding>(content);
                    Bindings.Add(binding);
                });
                files.Clear();
                HasPack = true; //设置是否有语言包
            }
            catch
            {
                throw new Exception($"加载语言文件出错(Error loading language file).");
            }
        }

        /// <summary>
        /// 获取资源文件内容
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private static List<string> GetResources(string file)
        {
            var rescources = new List<string>();
            var buffer = File.ReadAllBytes(file);
            var assembly = Assembly.Load(buffer);
            var names = assembly.GetManifestResourceNames().ToList();
            if (names.Count == 0)
                return rescources;
            names.ForEach(name =>
             {
                 lock (locker)
                 {
                     if (name.EndsWith(".json"))
                     {
                         using (var stream = assembly.GetManifestResourceStream(name))
                         {
                             if (stream != null)
                             {
                                 var block = new byte[stream.Length];
                                 stream.Read(block, 0, block.Length);
                                 stream.Position = 0;
                                 StreamReader reader = new StreamReader(stream);
                                 string content = reader.ReadToEnd();
                                 rescources.Add(content);
                             }
                         }
                     }
                 }
             });
            return rescources;
        }

        /// <summary>
        /// 获取语言包dll文件
        /// </summary>
        /// <returns></returns>
        private static string GetPackFile()
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin");
            var searchPattern = string.Format("{0}.dll", LANGUAGE_PACKS.Replace(".dll", ""));
            var packFile = Directory.GetFiles(path, searchPattern).FirstOrDefault();
            return packFile;
        }
        #endregion
    }

}
