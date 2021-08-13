using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CascadeUITest
{
    public static class AssertLog
    {
        /// <summary>
        /// логирует скрин к отчету Allure.
        /// </summary>
        /// <param name="condition">Условие</param>
        /// <param name="message">Сообщение, если условие ложно</param>
        /// <param name="driver">Драйвер</param>
        /// <exception cref="AssertionException"></exception>
        public static void True(bool condition, string message, ManagerApp driver)
        {
            try
            {
                Assert.True(condition, message);
            }
            catch (AssertionException)
            { 
                driver.AttachScreenToReport();
                throw;
            }
        }


        /// <summary>
        /// логирует скрин к отчету Allure.
        /// </summary>
        /// <param name="condition">Условие</param>
        /// <param name="driver">Драйвер</param>
        /// <exception cref="AssertionException"></exception>
        public static void True(bool condition, ManagerApp driver)
        {
            try
            {
                Assert.That(condition);
            }
            catch (AssertionException)
            {
                driver.AttachScreenToReport();
                throw;
            }
        }

        /// <summary>
        /// Прикрепить скриншот к отчету Allure, если во время теста возникнет необработанная ошибка
        /// </summary>
        /// <param name="driver">Экземпляр драйвера</param>
        /// <param name="tryAction">Блок с тестом</param>
        /// <param name="catchAction">Дополнительная логика при возникновении ошибки</param>
        public static void LogIfError(ManagerApp driver, Action tryAction, Action catchAction = null)
        {
            try
            {
                tryAction.Invoke();
            }
            catch (Exception)
            {
                catchAction?.Invoke();
                driver.AttachScreenToReport();
                //var errors = WebPage.TryGetErrors(driver);
                //if(errors!=string.Empty)
                //    driver.AttachResToReport("Лог консоли: "+errors, "text/html", "html");
                throw;
            }
        }
    }
}