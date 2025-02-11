using Newtonsoft.Json;

namespace RESTfulNetCoreWebAPI_TicketList.Extensions
{
    public static class ExtensionsMethods
    {
        /// <summary>
        ///   Extension method that creates a deep copy (clone) of an instance of object (thisOne) 
        ///   by serializing and then returning a deserialized copy. The original object instance
        ///   is therefore not mutated.
        /// </summary>
        /// <typeparam name="T">Generic type parameter</typeparam>
        /// <param name="thisOne">Object to be copied</param>
        /// <returns>New instance of a deep copy of an object</returns>
        /// <exception cref="ArgumentNullException">If the object is null</exception>
        public static T DeepCopy<T>(this T thisOne)
        {
            var jsonSerialized = JsonConvert.SerializeObject(thisOne);
            return JsonConvert.DeserializeObject<T>(jsonSerialized) ?? throw new ArgumentNullException();
        }
    }
}
