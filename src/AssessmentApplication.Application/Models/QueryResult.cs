namespace AssessmentApplication.Application.Models
{
    /// <summary>
    /// Represents the result and result type of a query.
    /// </summary>
    /// <typeparam name="T">The type of the result object.</typeparam>
    public class QueryResult<T>
    {
        /// <summary>
        /// The result of the query
        /// </summary>
        public QueryResultType QueryResultType { get; set; }

        /// <summary>
        /// The returned result object
        /// </summary>
        public T Result { get; set; }
    }
}
