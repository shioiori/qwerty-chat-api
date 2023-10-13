using qwerty_chat_api.Domain.Attributes;

namespace qwerty_chat_api.Domain.Utils
{
    public class CollectionUtils<T>
    {
        public static string GetCollectionName()
        {
            return (typeof(T).GetCustomAttributes(typeof(BsonCollectionAttribute), true).FirstOrDefault()
                as BsonCollectionAttribute).CollectionName;
        }
    }
}
