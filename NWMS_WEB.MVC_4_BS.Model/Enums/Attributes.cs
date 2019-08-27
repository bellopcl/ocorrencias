using System;
using System.Linq;
using System.Reflection;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public static class Attributes
    {
        /// <summary>
        /// Classe que representa um atributo para armazenar um valor do tipo objeto. 
        /// </summary>
        [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
        public class KeyValueAttribute : Attribute
        {
            /// <summary>
            /// Nome da chave de valor que complementa um código de atributo.
            /// </summary>
            private const string KeyNameEnumValue = "__KEY_NAME_ENUM_VALUE__";

            /// <summary>
            /// Inicia uma nova instância da classe <see cref="KeyValueAttribute"/>.
            /// </summary>
            /// <param name="key">Nome da chave.</param>
            /// <param name="value">Valor da chave.</param>
            public KeyValueAttribute(object key, object value)
            {
                this.Name = key;
                this.Value = value;
            }

            /// <summary>
            /// Inicia uma nova instância da classe <see cref="KeyValueAttribute"/>.
            /// </summary>
            /// <param name="value">Valor da chave.</param>
            /// <param name="enumValue">código do atributo</param>
            public KeyValueAttribute(object value, bool enumValue)
            {
                if (enumValue)
                {
                    this.Name = KeyNameEnumValue;
                }

                this.Value = value;
            }

            /// <summary>
            /// Obtém Nome da chave em conversão.
            /// </summary>
            public object Name { get; private set; }

            /// <summary>
            /// Obtém Valor da chave em conversão.
            /// </summary>
            public object Value { get; private set; }

            /// <summary>
            /// Obtém um valor que indica se a chave é valor de complemento de um código de atributo.
            /// </summary>
            public bool IsEnumValue
            {
                get
                {
                    return this.Name.Equals(KeyNameEnumValue);
                }
            }

            /// <summary>
            /// Pega todos atributos do tipo <see cref="KeyValueAttribute"/> de um campo código de atributo.
            /// </summary>
            /// <param name="enumField">Referência do campo código do atributo.</param>
            /// <returns>Retorna uma lista com os objetos.</returns>
            public static KeyValueAttribute[] Get(Enum enumField)
            {
                if (enumField == null)
                {
                    return null;
                }
                else
                {
                    Type type = enumField.GetType();
                    FieldInfo enumInfo = type.GetField(enumField.ToString());
                    return enumInfo.GetCustomAttributes(typeof(KeyValueAttribute), false) as KeyValueAttribute[];
                }
            }

            /// <summary>
            /// Pega o valor do primeiro atributo definido como código de atributo igual a TRUE.
            /// </summary>
            /// <param name="enumField">Referência do campo código do atributo.</param>
            /// <returns>Retorna o objeto que representa o valor do código do atributo.</returns>
            public static object GetEnumValue(Enum enumField)
            {
                if (enumField == null)
                {
                    return null;
                }
                else
                {
                    return Get(enumField).First(m => m.IsEnumValue).Value;
                }
            }

            /// <summary>
            /// Pega o valor do primeiro atributo definido como código de atributo igual a TRUE
            /// e faz a conversão para o tipo desejado.
            /// </summary>
            /// <typeparam name="T">Referência de tipo para conversão.</typeparam>
            /// <param name="enumField">Referência do campo código do atributo.</param>
            /// <returns>Retorna o objeto que representa o valor do código do atributo.</returns>
            public static T GetEnumValue<T>(Enum enumField)
            {
                return (T)GetEnumValue(enumField);
            }

            /// <summary>
            /// Pega o primeiro atributo do tipo <see cref="KeyValueAttribute"/> definido.
            /// </summary>
            /// <param name="enumField">Referência do campo código do atributo.</param>
            /// <returns>Retorna o objeto que representa o valor do código do atributo.</returns>
            public static KeyValueAttribute GetFirst(Enum enumField)
            {
                return Get(enumField).First();
            }

            /// <summary>
            /// Pega o primeiro atributo do tipo <see cref="KeyValueAttribute"/> definido ou o valor default.
            /// </summary>
            /// <param name="enumField">Referência do campo código do atributo.</param>
            /// <returns>Retorna o objeto que representa o valor do código do atributo.</returns>
            public static KeyValueAttribute GetFirstOrDefault(Enum enumField)
            {
                return Get(enumField).FirstOrDefault();
            }

            /// <summary>
            /// Pega  a partir do nome uma lista de atributos do tipo <see cref="KeyValueAttribute"/> definido.
            /// </summary>
            /// <param name="key">Nome do atributo.</param>
            /// <param name="enumField">Referência do campo código do atributo.</param>
            /// <returns>Retorna lista de <see cref="KeyValueAttribute"/>.</returns>
            public static KeyValueAttribute[] Get(string key, Enum enumField)
            {
                return Get(enumField).Where(m => m.Name.Equals(key)).ToArray();
            }

            /// <summary>
            /// Pega a partir do nome o primeiro atributo do tipo <see cref="KeyValueAttribute"/> definido.
            /// </summary>
            /// <param name="key">Nome do atributo.</param>
            /// <param name="enumField">Referência do campo código do atributo.</param>
            /// <returns>Retorna <see cref="KeyValueAttribute"/>.</returns>
            public static KeyValueAttribute GetFirst(string key, Enum enumField)
            {
                return Get(key, enumField).First(m => m.Name.Equals(key));
            }

            /// <summary>
            /// Pega a partir do nome o primeiro atributo ou default do tipo <see cref="KeyValueAttribute"/> definido.
            /// </summary>
            /// <param name="key">Nome do atributo.</param>
            /// <param name="enumField">Referência do campo código do atributo.</param>
            /// <returns>Retorna <see cref="KeyValueAttribute"/>.</returns>
            public static KeyValueAttribute GetFirstOrDefault(string key, Enum enumField)
            {
                return Get(key, enumField).FirstOrDefault(m => m.Name.Equals(key));
            }

            /// <summary>
            /// Pega o valor convertendo.
            /// </summary>
            /// <returns>Retorna valor em conversão.</returns>
            public object GetValue()
            {
                return this.Value;
            }

            /// <summary>
            /// Pega o valor convertendo para o tipo informado(T).
            /// </summary>
            /// <typeparam name="T">Tipo de saída.</typeparam>
            /// <returns>convertendo no tipo T.</returns>
            public T GetValue<T>()
            {
                return (T)this.GetValue();
            }

            /// <summary>
            /// Tenta fazer a conversão para o tipo informado(T). 
            /// </summary>
            /// <typeparam name="T">Tipo de saída.</typeparam>
            /// <param name="value">Referência que irá alocar a saída.</param>
            /// <returns>Status do conversão finalizado.</returns>
            public bool TryGetValue<T>(ref T value)
            {
                try
                {
                    value = (T)this.GetValue<T>();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
