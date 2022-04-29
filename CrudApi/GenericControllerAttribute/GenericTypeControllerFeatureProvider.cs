using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CrudApi.Controllers;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace CrudApi.GenericControllerAttribute
{
    /// <summary>
    /// использование инфраструктуры mvc для создания типизированных обобщенных контроллеров,
    /// т.к. по умолчанию asp.net не видит контроллер EntityControllerTEntity и нужно либо создавать
    /// вручную наследуемые от базового обобщенного контроллера типизированные контроллеры, либо
    /// использовать автоматизированный механизм как в данном случае (удобно, если моделей будет очень много)
    /// </summary>
    public class GenericTypeControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
    {
        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
        {
            var currentAssembly = typeof(GenericTypeControllerFeatureProvider).Assembly;
            var candidates = currentAssembly.GetExportedTypes()
                .Where(x => x.GetCustomAttributes<GeneratedControllerAttribute>().Any()); // получаем все типы в сборке,
            // которые имеют данный атрибут
            
            foreach (var candidate in candidates) // производим перебор по всем найденным типам
            {
                feature.Controllers.Add( // создаем новые обобщенные контроллеры и типизируем всеми найденными типами
                                         // с атрибутом GeneratedControllerAttribute
                    typeof(EntityController<>).MakeGenericType(candidate).GetTypeInfo()
                );
            }
        }
    }
}