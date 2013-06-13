/* Copyright 2010-2013 10gen Inc.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
* http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*/

namespace MongoDB.Driver.Linq.Remove
{
    using System;
    using System.Linq.Expressions;

    /// <summary>
    /// Static class that contains the extension methods that allow you
    /// to define remove queries with LINQ expressions.
    /// </summary>
    public static class RemoveExtensionMethods
    {
        /// <summary>
        /// Removes documents from this collection that match a query.
        /// </summary>
        /// <typeparam name="T">The type of the removed documents.</typeparam>
        /// <param name="collection">The collection that documents will be removed from.</param>
        /// <param name="queryExpression">The query expression that defines which documents will be removed.</param>
        /// <returns>A WriteConcernResult (or null if WriteConcern is disabled).</returns>
        public static WriteConcernResult Remove<T>(            
            this MongoCollection<T> collection,
            Expression<Func<T, bool>> queryExpression)
        {
            var query = LinqToMongo.CreateQueryFromExpression(collection, queryExpression);
            return collection.Remove(query);
        }

        /// <summary>
        /// Removes documents from this collection that match a query.
        /// </summary>
        /// <typeparam name="T">The type of the removed documents.</typeparam>
        /// <param name="collection">The collection that documents will be removed from.</param>
        /// <param name="queryExpression">The query expression that defines which documents will be removed.</param>
        /// <param name="writeConcern">The write concern to use for this Insert.</param>
        /// <returns>A WriteConcernResult (or null if WriteConcern is disabled).</returns>
        public static WriteConcernResult Remove<T>(
            this MongoCollection<T> collection,
            Expression<Func<T, bool>> queryExpression,
            WriteConcern writeConcern)
        {
            var query = LinqToMongo.CreateQueryFromExpression(collection, queryExpression);
            return collection.Remove(query, writeConcern);
        }

        /// <summary>
        /// Removes documents from this collection that match a query.
        /// </summary>
        /// <typeparam name="T">The type of the removed documents.</typeparam>
        /// <param name="collection">The collection that documents will be removed from.</param>
        /// <param name="queryExpression">The query expression that defines which documents will be removed.</param>
        /// <param name="flags">The flags for this Remove (see <see cref="RemoveFlags"/>).</param>
        /// <returns>A WriteConcernResult (or null if WriteConcern is disabled).</returns>
        public static WriteConcernResult Remove<T>(
            this MongoCollection<T> collection,
            Expression<Func<T, bool>> queryExpression,
            RemoveFlags flags)
        {
            var query = LinqToMongo.CreateQueryFromExpression(collection, queryExpression);
            return collection.Remove(query, flags);
        }

        /// <summary>
        /// Removes documents from this collection that match a query.
        /// </summary>
        /// <typeparam name="T">The type of the removed documents.</typeparam>
        /// <param name="collection">The collection that documents will be removed from.</param>
        /// <param name="queryExpression">The query expression that defines which documents will be removed.</param>
        /// <param name="flags">The flags for this Remove (see <see cref="RemoveFlags"/>).</param>
        /// <param name="writeConcern">The write concern to use for this Insert.</param>
        /// <returns>A WriteConcernResult (or null if WriteConcern is disabled).</returns>
        public static WriteConcernResult Remove<T>(
            this MongoCollection<T> collection,
            Expression<Func<T, bool>> queryExpression,
            RemoveFlags flags, 
            WriteConcern writeConcern)
        {
            var query = LinqToMongo.CreateQueryFromExpression(collection, queryExpression);
            return collection.Remove(query, flags, writeConcern);
        }
    }
}
