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
using MongoDB.Driver.Linq.Remove;

namespace MongoDB.DriverUnitTests.Linq.Remove
{
    using System;
    using MongoDB.Driver;
    using MongoDB.Driver.Builders;

    [TestFixture]
    public class RemoveExtensionsTest
    {
        private class TestClass
        {
            public BsonObjectId Id { get; set; }
            public string Name { get; set; }
            public string Summary { get; set; }
        }

        [Test]
        public void TestSimpleRemove()
        {
            var collection = Configuration.GetTestCollection<TestClass>();
            collection.Drop();

            collection.InsertBatch(
                new[]
                    {
                        new TestClass { Name = "U1", Summary = "Summary1" },
                        new TestClass { Name = "U2", Summary = "Summary2" },
                    });
            var result = collection.Remove(t => t.Name == "U1");

            Assert.AreEqual(1, result.DocumentsAffected);
            Assert.AreEqual(1, collection.Count());
            Assert.AreEqual("U2", collection.FindOne().Name);
        }

        [Test]
        public void TestSingleRemovesOnlyOne()
        {
            var collection = Configuration.GetTestCollection<TestClass>();
            collection.Drop();

            collection.InsertBatch(
                new[]
                    {
                        new TestClass { Name = "U1", Summary = "Summary1" },
                        new TestClass { Name = "U1", Summary = "Summary1" },
                        new TestClass { Name = "U1", Summary = "Summary1" },
                        new TestClass { Name = "U1", Summary = "Summary1" },
                    });
            var result = collection.Remove(t => t.Name == "U1", RemoveFlags.Single);

            Assert.AreEqual(1, result.DocumentsAffected);
            Assert.AreEqual(3, collection.Count());
        }

        [Test]
        public void TestMultiRemove()
        {
            var collection = Configuration.GetTestCollection<TestClass>();
            collection.Drop();
            collection.InsertBatch(
                new[]
                    {
                        new TestClass { Name = "U1", Summary = "Summary1" },
                        new TestClass { Name = "U1", Summary = "Summary1" },
                        new TestClass { Name = "U2", Summary = "Summary2" },
                        new TestClass { Name = "U1", Summary = "Summary1" },
                    });

            var result = collection.Remove(t => t.Name == "U1");
            Assert.AreEqual(3, result.DocumentsAffected);
            Assert.AreEqual("Summary2", collection.FindOne().Summary);
        }

        [Test]
        public void TestRemoveShouldCrashOnBadExpression()
        {
            var collection = Configuration.GetTestCollection<TestClass>();
            collection.Drop();

            Assert.Throws<NotSupportedException>(() =>
                collection.Remove(t => t.Name.GetEnumerator().Equals(typeof(Query)))
                );
        }
    }
}
