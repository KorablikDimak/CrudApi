using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace CrudApi.GenericControllerAttribute
{
    /// <summary>
    /// перебираем все контроллеры, и если они универсального типа, то
    /// подбираем маршрут из атрибута и встраиваем его в этот контроллер
    /// </summary>
    public class GenericControllerRouteConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            if (!controller.ControllerType.IsGenericType) return;
            
            var genericType = controller.ControllerType.GenericTypeArguments[0]; // контроллер типизируется только одним типом
            var customNameAttribute = genericType.GetCustomAttribute<GeneratedControllerAttribute>();

            if (customNameAttribute?.Route != null)
            {
                controller.Selectors.Add(new SelectorModel
                {
                    AttributeRouteModel = new AttributeRouteModel(new RouteAttribute(customNameAttribute.Route)),
                });
            }
        }
    }
}