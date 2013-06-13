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

using MongoDB.Bson;
using NUnit.Framework;
using MongoDB.Driver.Linq.Updates;

namespace MongoDB.DriverUnitTests.Linq.Updates
{
    using System;
    using MongoDB.Driver;
    using MongoDB.Driver.Builders;
    using MongoDB.Driver.Linq;

    [TestFixture]
    public class UpdateExtensionsTest
    {
        private class TestClass
        {
            public BsonObjectId Id { get; set; }
            public string Name { get; set; }
            public string Summary { get; set; }
        }

        [Test]
        public void TestSimpleUpdate()
        {
            var collection = Configuration.GetTestCollection<TestClass>();
            collection.Drop();

            collection.InsertBatch(
                new[]
                    {
                        new TestClass { Name = "U1", Summary = "Summary1" },
                        new TestClass { Name = "U2", Summary = "Summary2" },
                    });
            var update = Update<TestClass>.Set(t => t.Summary, "Updated summary");

            var result = collection.Update(t => t.Name == "U1", update);
            
            Assert.AreEqual(1, result.DocumentsAffected);
            Assert.AreEqual("Updated summary", collection.FindOne(Query.EQ("Name", "U1")).Summary);
            Assert.AreEqual("Summary2", collection.FindOne(Query.EQ("Name", "U2")).Summary);
        }

        [Test]
        public void TestMultiUpdate()
        {
            var collection = Configuration.GetTestCollection<TestClass>();
            collection.Drop();
            collection.InsertBatch(
                new[]
                    {
                        new TestClass { Name = "U1", Summary = "Summary1" },
                        new TestClass { Name = "U2", Summary = "Summary2" },
                    });
            var update = Update<TestClass>.Set(t => t.Summary, "Updated summary");

            var result = collection.Update(t => t.Name.In(new[]{"U1", "U2"}), update, UpdateFlags.Multi);

            Assert.AreEqual(2, result.DocumentsAffected);
            Assert.AreEqual("Updated summary", collection.FindOne(Query.EQ("Name", "U1")).Summary);
            Assert.AreEqual("Updated summary", collection.FindOne(Query.EQ("Name", "U2")).Summary);
        }

        [Test]
        public void TestUpdateShouldCrashOnBadExpression()
        {
            var collection = Configuration.GetTestCollection<TestClass>();
            collection.Drop();

            var update = Update<TestClass>.Set(t => t.Summary, "Updated summary");

            Assert.Throws<NotSupportedException>(() =>
                collection.Update(t => t.Name.GetEnumerator().Equals(typeof(Query)), update, UpdateFlags.Multi)
                );
        }
    }
}
