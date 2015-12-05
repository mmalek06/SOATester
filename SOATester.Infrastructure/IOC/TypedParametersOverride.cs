using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;

namespace SOATester.Infrastructure.IOC {
    public class TypedParametersOverride : ResolverOverride {
        private readonly Dictionary<Type, InjectionParameterValue> parameterValues;

        public TypedParametersOverride(IEnumerable<object> parameterValues) {
            this.parameterValues = new Dictionary<Type, InjectionParameterValue>();

            foreach (var parameterValue in parameterValues) {
                this.parameterValues[parameterValue.GetType()] = InjectionParameterValue.ToParameter(parameterValue);
            }
        }

        public override IDependencyResolverPolicy GetResolver(IBuilderContext context, Type dependencyType) {
            if (parameterValues.Count < 1) {
                return null;
            }

            var value = parameterValues[dependencyType];

            return value.GetResolverPolicy(dependencyType);
        }
    }
}
