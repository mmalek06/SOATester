using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;

namespace SOATester.Infrastructure.IOC {
    public class TypedParametersOverride : ResolverOverride {
        private readonly Dictionary<Type, InjectionParameterValue> parameterValues = new Dictionary<Type, InjectionParameterValue>();

        public TypedParametersOverride(object parameterValue, Type asType) {
            parameterValues[asType] = InjectionParameterValue.ToParameter(parameterValue);
        }

        public TypedParametersOverride(object parameterValue) {
            parameterValues[parameterValue.GetType()] = InjectionParameterValue.ToParameter(parameterValue);
        }

        public TypedParametersOverride(IEnumerable<object> parameterValues) {
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
