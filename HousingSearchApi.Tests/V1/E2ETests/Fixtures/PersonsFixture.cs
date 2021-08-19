using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using AutoFixture;
using Elasticsearch.Net;
using HousingSearchApi.Tests.V1.E2ETests.Fixtures.DataHelpers;
using HousingSearchApi.Tests.V1.Helper;
using HousingSearchApi.V1.Gateways.Models;
using Nest;
// ReSharper disable All

namespace HousingSearchApi.Tests.V1.E2ETests.Fixtures
{
    public class PersonsFixture : BaseFixture
    {
        public List<QueryablePerson> Persons { get; private set; }
        private const string INDEX = "persons";
        public static string[] Alphabet = { "aa", "bb", "cc", "dd", "ee", "vv", "ww", "xx", "yy", "zz" };

        public PersonsFixture(IElasticClient elasticClient, HttpClient httpClient) : base(elasticClient, httpClient)
        {
            WaitForESInstance();
        }

        public void GivenAPersonIndexExists()
        {
            if (!ElasticSearchClient.Indices.Exists(Indices.Index(INDEX)).Exists)
            {
                var personSettingsDoc = File.ReadAllTextAsync("./data/elasticsearch/tenureIndex.json").Result;
                ElasticSearchClient.LowLevel.Indices.CreateAsync<BytesResponse>(INDEX, personSettingsDoc)
                    .ConfigureAwait(true);

                var persons = CreatePersonData();
                ElasticSearchClient.IndexManyAsync(persons, INDEX);

                var timeout = DateTime.UtcNow.AddSeconds(10); // 10 second timeout to make sure all the data is there.

                while (DateTime.UtcNow < timeout)
                {
                    var count = ElasticSearchClient.Cluster.Stats().Indices.Documents.Count;
                    if (count >= persons.Count)
                        break;

                    Thread.Sleep(200);
                }
            }
        }

        private List<QueryablePerson> CreatePersonData()
        {
            var listOfPersons = new List<QueryablePerson>();
            var random = new Random();
            var fixture = new Fixture();

            // Make sure there are 10 of each surname first in case there are tests that depend on some being there.
            foreach (var name in Alphabet)
            {
                var persons = fixture.Build<QueryablePerson>()
                                    .With(x => x.Firstname, "Some first name")
                                    .With(x => x.Surname, name)
                                    .CreateMany(10);

                listOfPersons.AddRange(persons);
            }

            // Add loads more at random
            for (int i = 0; i < 900; i++)
            {
                var person = fixture.Create<QueryablePerson>();

                var firstName = Alphabet[random.Next(0, Alphabet.Length)];
                person.Firstname = firstName;

                var lastName = Alphabet[random.Next(0, Alphabet.Length)];
                person.Surname = lastName;

                listOfPersons.Add(person);
            }

            var lastPerson = fixture.Create<QueryablePerson>();
            lastPerson.Firstname = Alphabet.Last();
            lastPerson.Surname = Alphabet.Last();

            var firstPerson = fixture.Create<QueryablePerson>();
            firstPerson.Firstname = Alphabet.First();
            firstPerson.Surname = Alphabet.First();

            listOfPersons.Add(lastPerson);
            listOfPersons.Add(firstPerson);

            return listOfPersons;
        }
    }
}
