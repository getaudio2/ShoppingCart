using Newtonsoft.Json;

namespace ShoppingCart.Infrastructure
{
    // La clase SessionExtensions hace uso de ISession para guardar datos
    // en un almacén mantenido por la aplicación web mientras se navega por el usuario
    // En este caso, ISession se usa para almacenar temporalmente (similar a las cookies)
    // los datos de los productos comprados en la tienda
    public static class SessionExtensions
    {
        public static void SetJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetJson<T>(this ISession session, string key)
        {
            var sessionData = session.GetString(key);
            return sessionData == null ? default : JsonConvert.DeserializeObject<T>(sessionData);
        }
    }
}
