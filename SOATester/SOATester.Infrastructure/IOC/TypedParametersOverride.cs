using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;

namespace SOATester.Infrastructure.IOC {
    public class TypedParametersOverride : ResolverOverride {
        private readonly Dictionary<Type, InjectionParameterValue> _parameterValues;

        public TypedParametersOverride(IEnumerable<object> parameterValues) {
            _parameterValues = new Dictionary<Type, InjectionParameterValue>();

            foreach (var parameterValue in parameterValues) {
                _parameterValues[parameterValue.GetType()] = InjectionParameterValue.ToParameter(parameterValue);
            }
        }

        public override IDependencyResolverPolicy GetResolver(IBuilderContext context, Type dependencyType) {
            if (_parameterValues.Count < 1) {
                return null;
            }

            var value = _parameterValues[dependencyType];

            return value.GetResolverPolicy(dependencyType);
        }
    }
}
