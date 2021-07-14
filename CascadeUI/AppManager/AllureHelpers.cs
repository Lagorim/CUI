using System;
using System.IO;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;

namespace CascadeUITest
{
    /// <summary>
    ///     Содержит вспомогательные статические свойства и методы, используемые в проекте.
    /// </summary>
    internal static class AllureHelpers
    {
        public static ConfigReader reader = new ConfigReader();
        private static char dirSeparator = Path.DirectorySeparatorChar;
        
        #region Информация о системе
        public static string MachineName = Environment.MachineName; // Имя тачки
        public static string OsUserName = Environment.UserName; // Имя пользователя ОС
        public static string OsVersion = Environment.OSVersion.VersionString;
        public static string OsArchitecture = Environment.Is64BitOperatingSystem ? "64 бит" : "32 бит";
        public static string ProcessorCount = Environment.ProcessorCount.ToString();
        public static string baseUrl = reader.GetValue("AppURL");
        #endregion
        /// <summary>
        ///     Добавляет environment в отчет Allure
        /// </summary>
        public static void AddEnvironment()
        {
            var resultsPath = Path.GetFullPath(
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                    $"..{dirSeparator}..{dirSeparator}.."
                ));
            Directory.CreateDirectory($"{resultsPath}{dirSeparator}results");

            // environment.xml
            var environment = new XDocument(new XElement("environment",
                new XElement("parameter",
                    new XAttribute("key", "Адрес стенда"),
                    new XAttribute("value", $"{baseUrl}")),
                new XElement("parameter",
                    new XAttribute("key", "Имя агента"),
                    new XAttribute("value", $"{MachineName}")),
                new XElement("parameter",
                    new XAttribute("key", "Имя пользователя ОС"),
                    new XAttribute("value", $"{OsUserName}")),
                new XElement("parameter",
                    new XAttribute("key", "ОС агента"),
                    new XAttribute("value", $"{OsVersion}")),
                new XElement("parameter",
                    new XAttribute("key", "Тип ОС"),
                    new XAttribute("value", $"{OsArchitecture}")),
                new XElement("parameter",
                    new XAttribute("key", "Количество ядер процессора"),
                    new XAttribute("value", $"{ProcessorCount}"))));

            environment.Save($"{resultsPath}{dirSeparator}results{dirSeparator}environment.xml");
        }

        /// <summary>
        ///     Добавляет product defects в отчет Allure
        /// </summary>
        public static void AddDefects()
        {
            var resultsPath = Path.GetFullPath(
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                    $"..{dirSeparator}..{dirSeparator}.."
                ));
            Directory.CreateDirectory($"{resultsPath}{dirSeparator}results");

            // categories.json
            var categories = new JArray();
            
            categories.Add(new JObject(
                new JProperty("name", "Не удалось кликнуть на элемент"),
                new JProperty("matchedStatuses", new JArray("failed")),
                new JProperty("messageRegex", ".*Other element would receive the click:.*")));
            
            categories.Add(new JObject(
                new JProperty("name", "Элемент не найден на странице"),
                new JProperty("matchedStatuses", new JArray("failed")),
                new JProperty("traceRegex", ".*NoSuchElementException.*")));
            
            categories.Add(new JObject(
                new JProperty("name", "Вышло время ожидания"),
                new JProperty("matchedStatuses", new JArray("failed")),
                new JProperty("messageRegex", ".*WebDriverTimeoutException.*")));
            
            categories.Add(new JObject(
                new JProperty("name", "Ожидание не соответствует результату"),
                new JProperty("matchedStatuses", new JArray("failed")),
                new JProperty("messageRegex", ".*Expected:.*")));

            categories.Add(new JObject(
                new JProperty("name", "Пропущенные тесты"),
                new JProperty("matchedStatuses", new JArray("skipped"))));
            
            categories.Add(new JObject(
                new JProperty("name", "Упавшие тесты"),
                new JProperty("matchedStatuses", new JArray("failed"))));
            
            categories.Add(new JObject(
                new JProperty("name", "Проблемы с тестом"),
                new JProperty("matchedStatuses", new JArray("broken"))));
            
            File.WriteAllText($"{resultsPath}{dirSeparator}results{dirSeparator}categories.json", categories.ToString());
        }
    }
}