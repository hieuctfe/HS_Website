namespace RealEstate.Infrastructure
{
    using Newtonsoft.Json;

    public static class ObjectExtension
    {
        public static T Clone<T>(this T entity)
            => JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(entity, new JsonSerializerSettings
            {
                ObjectCreationHandling = ObjectCreationHandling.Replace,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            }));
    }
}