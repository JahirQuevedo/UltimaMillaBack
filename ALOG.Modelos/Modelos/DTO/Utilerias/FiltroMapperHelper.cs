using System.Reflection;

namespace ALOG.Modelos.Modelos.DTO.Utilerias
{
    public static class FiltroMapperHelper
    {
        public static bool TryObtenerFiltros<T>(Dictionary<string, string> dic, out T resultado) where T : new()
        {
            resultado = new T();
            var tipo = typeof(T);
            bool todoCorrecto = true;

            foreach (var prop in tipo.GetProperties())
            {
                var key = dic.Keys.FirstOrDefault(k => string.Equals(k, prop.Name, StringComparison.OrdinalIgnoreCase));
                if (key == null)
                {
                    if (prop.PropertyType.IsValueType && Nullable.GetUnderlyingType(prop.PropertyType) == null && prop.PropertyType != typeof(bool))
                        todoCorrecto = false;

                    continue;
                }

                var valorStr = dic[key];
                try
                {
                    object? valorConvertido = null;

                    if (prop.PropertyType == typeof(string))
                        valorConvertido = valorStr;

                    else if (prop.PropertyType == typeof(int) || prop.PropertyType == typeof(int?))
                        valorConvertido = int.TryParse(valorStr, out var val) ? val : GetDefault(prop, ref todoCorrecto);

                    else if (prop.PropertyType == typeof(bool) || prop.PropertyType == typeof(bool?))
                        valorConvertido = bool.TryParse(valorStr, out var val) ? val : GetDefault(prop, ref todoCorrecto);

                    else if (prop.PropertyType == typeof(DateTime) || prop.PropertyType == typeof(DateTime?))
                        valorConvertido = DateTime.TryParse(valorStr, out var val) ? val : GetDefault(prop, ref todoCorrecto);

                    if (valorConvertido != null)
                        prop.SetValue(resultado, valorConvertido);
                }
                catch
                {
                    todoCorrecto = false;
                }
            }

            return todoCorrecto;
        }

        private static object? GetDefault(PropertyInfo prop, ref bool todoCorrecto)
        {
            if (Nullable.GetUnderlyingType(prop.PropertyType) != null)
                return null;

            todoCorrecto = false;
            return Activator.CreateInstance(prop.PropertyType);
        }

        public static string ConvertirFiltroAString(object filtro)
        {
            if (filtro == null)
                return string.Empty;

            var tipo = filtro.GetType();
            var propiedades = tipo.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var pares = propiedades
                .Select(prop => new
                {
                    Nombre = prop.Name,
                    Valor = prop.GetValue(filtro)
                })
                .Where(x => x.Valor != null &&
                            !(x.Valor is string str && string.IsNullOrWhiteSpace(str)))
                .Select(x => $"{x.Nombre}={x.Valor}");

            return string.Join("&", pares);
        }
    }
}
