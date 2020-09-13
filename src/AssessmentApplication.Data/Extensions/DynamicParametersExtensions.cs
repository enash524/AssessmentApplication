using Dapper;

namespace AssessmentApplication.Data.Extensions
{
    public static class DynamicParametersExtensions
    {
        /// <summary>
        /// If an object is not null, add the object to the dynamic parameter collection with the parameter name. Do nothing if the object is null
        /// </summary>
        /// <param name="dynamicParameters">A bag of parameters that can be passed to the Dapper Query and Execute methods</param>
        /// <param name="parameterName">The name of the parameter.</param>
        /// <param name="nullableObject">The value of the parameter.</param>
        public static void AddNullableParameter(this DynamicParameters dynamicParameters, string parameterName, object nullableObject)
        {
            if (nullableObject != null)
            {
                dynamicParameters.Add(parameterName, nullableObject);
            }
        }
    }
}
