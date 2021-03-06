﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NSubstitute;
using NUnit.Framework;
using PeanutButter.Utils;

// Author notice: most of this is not written by me and may be subject to removal at the
//  behest of the original author: Brendon Page <brendonpage@live.co.za>

namespace PeanutButter.TestUtils.Generic
{
    public class ConstructorTestUtils
    {
        public static void ShouldExpectNonNullParameterFor<TCheckingConstructorOf>(string parameterName, Type expectedParameterType)
        {
            var constructor = GetConstructorInfo<TCheckingConstructorOf>();
            var parameters = GetConstructorParameters(parameterName, constructor);
            var parameter = parameters.FirstOrDefault(pi => pi.Name == parameterName);
            Assert.IsNotNull(parameter, "Unknown parameter for constructor of " + (typeof(TCheckingConstructorOf).PrettyName()) + ": " + parameterName);
            if (parameter.ParameterType != expectedParameterType)
                Assert.Fail(new[] 
                            { 
                                "Parameter ", 
                                parameterName, 
                                " is expected to have type: '",  
                                expectedParameterType.PrettyName(), 
                                "' but actually has type: '", 
                                parameter.ParameterType.PrettyName(), "'" 
                            }.JoinWith(string.Empty));

            var parameterValues = CreateParameterValues(parameterName, parameters.ToList());
            var thrownException = InvokeConstructor(constructor, parameterValues);
            var argumentNullException = AssertArgumentNullExceptionWasThrown(thrownException);
            Assert.AreEqual(parameterName, argumentNullException.ParamName);
        }

        private static ConstructorInfo GetConstructorInfo<T>()
        {
            var constructors = typeof (T).GetConstructors();
            if (constructors.Count() != 1)
            {
                throw new InvalidOperationException("This utility is designed to test classes with a single constructor.");
            }
            return constructors.FirstOrDefault();
        }

        private static IEnumerable<ParameterInfo> GetConstructorParameters(string parameterName, ConstructorInfo constructor)
        {
            var parameterInfos = constructor.GetParameters();
            if (parameterInfos.FirstOrDefault(info => info.Name == parameterName) == null)
            {
                throw new InvalidOperationException(
                    string.Format("The constructor didn't contain a parameter with the name '{0}'.",
                                  parameterName));
            }
            return parameterInfos;
        }

        private static IEnumerable<object> CreateParameterValues(string parameterName, List<ParameterInfo> parameters)
        {
            CheckParametersAreSubstitutable(parameters);
            return parameters.Select(parameterInfo => CreateParameterValue(parameterName, parameterInfo));
        }

        private static void CheckParametersAreSubstitutable(IEnumerable<ParameterInfo> parameters)
        {
            if (parameters.Any(info => !IsParameterSubstitutable(info)))
            {
                throw new InvalidOperationException(
                    "This utility is designed for constructors that only have parameters that can be substituted with NSubstitute.");
            }
        }

        private static bool IsParameterSubstitutable(ParameterInfo parameterInfo)
        {
            var parameterType = parameterInfo.ParameterType;
            return (parameterType.IsAbstract || parameterType.IsInterface
                    || parameterType.GetInterfaces().Any() || parameterType.IsClass)
                   && !parameterType.IsPrimitive;
        }

        private static object CreateParameterValue(string parameterName, ParameterInfo parameterInfo)
        {
            var parameterType = parameterInfo.ParameterType;

            object parameterValue = null;
            if (parameterInfo.Name != parameterName)
            {
                parameterValue = Substitute.For(new[] {parameterType}, new object[0]);
            }

            return parameterValue;
        }

        private static Exception InvokeConstructor(ConstructorInfo constructor, IEnumerable<object> parameterValues)
        {
            try
            {
                constructor.Invoke(parameterValues.ToArray());
                return null;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        private static ArgumentNullException AssertArgumentNullExceptionWasThrown(Exception thrownException)
        {
            var targetInvocationException = thrownException as TargetInvocationException;
            if (targetInvocationException == null)
            {
                ThrowArgumentNullExpectedException(thrownException);
            }

            // ReSharper disable PossibleNullReferenceException
            var argumentNullException = targetInvocationException.InnerException as ArgumentNullException;
            // ReSharper restore PossibleNullReferenceException
            if (argumentNullException == null)
            {
                ThrowArgumentNullExpectedException(targetInvocationException.InnerException);
            }

            return argumentNullException;
        }

        private static void ThrowArgumentNullExpectedException(Exception actualException)
        {
            var expectedValue = typeof (ArgumentNullException);
            var wasValue = (actualException == null) ? "Null" : actualException.GetType().ToString();
            throw new AssertionException(string.Format("Expected: {0} but was: {1}",
                                                       expectedValue,
                                                       wasValue));
        }
    }
}