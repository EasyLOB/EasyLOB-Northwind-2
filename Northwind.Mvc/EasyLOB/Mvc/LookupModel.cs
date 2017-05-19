using EasyLOB.Activity;
using System.Collections.Generic;

namespace EasyLOB.Mvc
{
    /// <summary>
    /// Lookup Model
    /// </summary>
    public class LookupModel
    {
        /// <summary>
        /// Operation Result.
        /// </summary>
        public ZOperationResult OperationResult { get; }

        /// <summary>
        /// Security Operations.
        /// </summary>
        public ZActivityOperations ActivityOperations { get; }

        /// <summary>
        /// Default lookup text.
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// Value Element HTML Id.
        /// </summary>
        public string ValueId { get; }

        /// <summary>
        /// HTML Elements to be uptaded after lookup selection.
        /// </summary>
        public List<LookupModelElement> Elements { get; }

        /// <summary>
        /// Query: ej.Query().where("Id", ej.FilterOperators.equal, 1)
        /// </summary>
        public string Query { get; }

        public LookupModel(ZActivityOperations activityOperations, string text, string valueId, List<LookupModelElement> elements = null, string query = null)
        {
            OperationResult = new ZOperationResult();

            ActivityOperations = activityOperations;
            Text = text ?? "";
            ValueId = valueId ?? "";
            Elements = elements ?? new List<LookupModelElement>();
            Query = query ?? "";
        }
    }

    /// <summary>
    /// Lookup HTML elements
    /// </summary>
    public class LookupModelElement
    {
        /// <summary>
        /// Element Id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Element associated property.
        /// </summary>
        public string Property { get; set; }

        public LookupModelElement()
        {
        }

        public LookupModelElement(string id, string property)
        {
            Id = id;
            Property = property;
        }
    }
}