using System.Data;
using Dapper;
using Newtonsoft.Json;

namespace API.Handlers
{
    public class DapperForSqliteTypeHandler<T> : SqlMapper.TypeHandler<T>
    {
        public override T Parse(object value)
        {
            Type getType;
            if (value == null)
            {
                return default;
            }
            else if (typeof(T).IsGenericType && typeof(T).GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                getType = Nullable.GetUnderlyingType(typeof(T));
            }
            else
            {
                getType = typeof(T);
            }

            if (Equals(Type.GetTypeCode(getType), TypeCode.Object))
            {
                if (getType == typeof(Guid))
                {
                    return (T)(object)Guid.Parse((string)value);
                }
                else
                {
                    return JsonConvert.DeserializeObject<T>((string)value);
                }
            }
            
            throw new Exception("沒有對應型別！");
        }

        public override void SetValue(IDbDataParameter parameter, T value)
        {
            if(!typeof(T).IsGenericType && typeof(T).GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                parameter.Value = JsonConvert.SerializeObject(value);
            }
            else
            {
                parameter.Value = value;
            }
        }
    }
}