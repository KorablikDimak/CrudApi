using System;

namespace CrudApi.GenericControllerAttribute
{
    /// <summary>
    /// кастомный атрибут, который используем со всеми типами, что нужно использовать в обобщенном контроллере.
    /// т.е. все из DataContext
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class GeneratedControllerAttribute : Attribute
    {
        public GeneratedControllerAttribute(string route)
        {
            Route = route;
        }
        
        // маршрут используется для создания шаблона api/entity/{название класса с данным атрибутом}
        public string Route { get; set; }
    }
}