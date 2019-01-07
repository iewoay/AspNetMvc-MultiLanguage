# AspNetMvc-MultiLanguage
基于Json文件的多语言方案
使用框架.NET Framework 4.5

## 1.LanguagePacks （语言包项目，包含json格式的语言文件。)：
json文件属性设置：生成操作修改为“嵌入的资源”。
注意，语言包项目不被其他项目引用。

json结构：
```json
{
"bindingName": "GLOBAL",
"mappers": [
    {
      "target": "你好",
      "languages": {
        "zh_cn": "你好",
        "en": "Hello",
        "jp": "こんにちは"
      }
    }]
}
```

## 2.MultiLanguage(多语言包处理项目)：
```csharp 
//初始化多语言方法（在Global/Application_Start中调用）
LanguageContext.Initialize()
```

## 3.WebAppDemo(测试网站项目)：
\Extensions\ControllerExtensions.cs提供了在控制器和Razor页面获取多语言内容的方法：
```csharp 
public static string GetLocale(this Controller controller, string target){
...
}
```
```csharp 
public static MvcHtmlString Locale(this HtmlHelper helper, string target){
...
}
```

## 4.具体使用方法：
控制器中：
```csharp 
ViewBag.Search =Locale("搜索");
ViewBag.Choose = Locale("请选择");
```
Razor页面：
```csharp 
<h1>@Html.Locale("你好")</h1>
<h1>@Html.Locale("新增")</h1>
<h1>@Html.Locale("修改")</h1>
<h1>@Html.Locale("删除")</h1>

<label>target设置为元素id</label>
<input  id="divMessage" value="@Html.Locale("divMessage")" />
```
```js
<!--脚本中使用-->
<script>
    var jsLanguages = { "title": "@Html.Locale("你好")" };
    alert(jsLanguages.title);
</script>
```
