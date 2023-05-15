using System.Text.Json;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace eShopApp.WebUI.Extensions.Serialization
{
    /// <summary>
    /// TempData icerisine kompleks tip obyekt yerlewdirmek ve oxumaq ucundur.
    /// </summary>
    public static class TempData
    {
        /// <summary>
        /// TempData icerisine kompleks tip data yerlewdirmek ucun hemin kompleks tipli datani serializasiya ederek yerlewdirir TempData-ya.
        /// </summary>
        /// <typeparam name="T">Seriallawdirilacaq olan tip.</typeparam>
        /// <param name="key">Yaradilacaq olan TempData-nin 'Key'-i ne olsun?</param>
        /// <param name="complexTypeData">TempData icerisine yerlewdireceyimiz kompleks tip data.</param>
        public static void Set<T>(this ITempDataDictionary tempData, string key, T complexTypeData) where T : class
        {
            tempData[key] = JsonSerializer.Serialize(complexTypeData);
        }

        /// <summary>
        /// TempData icerisinden kompleks tipli datani oxumaq ucun hemin kompleks tipli datani deserializasiya ederek oxuyur TempData-dan.
        /// </summary>
        /// <typeparam name="T">Deseriallawdirilacaq olan tip.</typeparam>
        /// <param name="key">TempData-dan deserializasiya ederek oxumaq istediyimiz ozunde kompleks tipli datani saxlayan Key nedir?</param>
        /// <returns>Verilmiw Key-e sahib datani TempData-dan taparaq dondurur, yox eger axtarilan Key tapilmasa bu zaman geriye null dondurulecek.</returns>
        public static T Get<T>(this ITempDataDictionary tempData, string key) where T : class
        {
            object data = tempData.Peek(key);
            return data == null ? null : JsonSerializer.Deserialize<T>((string)data);
        }
    }
}