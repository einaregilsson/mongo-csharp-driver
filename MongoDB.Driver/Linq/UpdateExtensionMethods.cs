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

namespace MongoDB.Driver.Linq.Updates
{
    using System;
    using System.Linq.Expressions;

    /// <summary>
    /// Static class that contains the extension methods that allow you
    /// to define update queries with LINQ expressions.
    /// </summary>
    public static class UpdateExtensionMethods
    {
        /// <summary>
        /// Updates one matching document in this collection.
        /// </summary>
        /// <typeparam name="T">The type of the updated documents.</typeparam>
        /// <param name="collection">The collection that will be updated.</param>
        /// <param name="queryExpression">A LINQ expression that matches the documents that should be updated.</param>
        /// <param name="update">The update to perform on the matching document.</param>
        /// <returns>A WriteConcernResult (or null if WriteConcern is disabled).</returns>
        public static WriteConcernResult Update<T>(
            this MongoCollection<T> collection,
            Expression<Func<T, bool>> queryExpression,
            IMongoUpdate update)
        {
            var query = LinqToMongo.CreateQueryFromExpression(collection, queryExpression);
            return collection.Update(query, update);
        }

        /// <summary>
        /// Updates one or more matching documents in this collection (for multiple updates use UpdateFlags.Multi).
        /// </summary>
        /// <typeparam name="T">The type of the updated documents.</typeparam>
        /// <param name="collection">The collection that will be updated.</param>
        /// <param name="queryExpression">A LINQ expression that matches the documents that should be updated.</param>
        /// <param name="update">The update to perform on the matching document.</param>
        /// <param name="options">The update options.</param>
        /// <returns>A WriteConcernResult (or null if WriteConcern is disabled).</returns>
        public static WriteConcernResult Update<T>(
            this MongoCollection<T> collection,
            Expression<Func<T, bool>> queryExpression,
            IMongoUpdate update,
            MongoUpdateOptions options)
        {
            var query = LinqToMongo.CreateQueryFromExpression(collection, queryExpression);
            return collection.Update(query, update, options);
        }

        /// <summary>
        /// Updates one matching document in this collection.
        /// </summary>
        /// <typeparam name="T">The type of the updated documents.</typeparam>
        /// <param name="collection">The collection that will be updated.</param>
        /// <param name="queryExpression">A LINQ expression that matches the documents that should be updated.</param>
        /// <param name="update">The update to perform on the matching document.</param>
        /// <param name="writeConcern">The write concern to use for this Insert.</param>
        /// <returns>A WriteConcernResult (or null if WriteConcern is disabled).</returns>
        public static WriteConcernResult Update<T>(
            this MongoCollection<T> collection,
            Expression<Func<T, bool>> queryExpression,
            IMongoUpdate update,
            WriteConcern writeConcern)
        {
            var query = LinqToMongo.CreateQueryFromExpression(collection, queryExpression);
            return collection.Update(query, update, writeConcern);
        }

        /// <summary>
        /// Updates one or more matching documents in this collection (for multiple updates use UpdateFlags.Multi).
        /// </summary>
        /// <typeparam name="T">The type of the updated documents.</typeparam>
        /// <param name="collection">The collection that will be updated.</param>
        /// <param name="queryExpression">A LINQ expression that matches the documents that should be updated.</param>
        /// <param name="update">The update to perform on the matching document.</param>
        /// <param name="flags">The flags for this Update.</param>
        /// <returns>A WriteConcernResult (or null if WriteConcern is disabled).</returns>
        public static WriteConcernResult Update<T>(
            this MongoCollection<T> collection,
            Expression<Func<T, bool>> queryExpression,
            IMongoUpdate update,
            UpdateFlags flags)
        {
            var query = LinqToMongo.CreateQueryFromExpression(collection, queryExpression);
            return collection.Update(query, update, flags);
        }

        /// <summary>
        /// Updates one or more matching documents in this collection (for multiple updates use UpdateFlags.Multi).
        /// </summary>
        /// <typeparam name="T">The type of the updated documents.</typeparam>
        /// <param name="collection">The collection that will be updated.</param>
        /// <param name="queryExpression">A LINQ expression that matches the documents that should be updated.</param>
        /// <param name="update">The update to perform on the matching document.</param>
        /// <param name="flags">The flags for this Update.</param>
        /// <param name="writeConcern">The write concern to use for this Insert.</param>
        /// <returns>A WriteConcernResult (or null if WriteConcern is disabled).</returns>
        public static WriteConcernResult Update<T>(
            this MongoCollection<T> collection,
            Expression<Func<T, bool>> queryExpression,
            IMongoUpdate update,
            UpdateFlags flags,
            WriteConcern writeConcern)
        {
            var query = LinqToMongo.CreateQueryFromExpression(collection, queryExpression);
            return collection.Update(query, update, flags, writeConcern);
        }
    }
}
