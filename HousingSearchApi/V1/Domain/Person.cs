using System.Collections.Generic;
using HousingSearchApi.V1.Domain.ES;

namespace HousingSearchApi.V1.Domain
{
    public class Person
    {
        public static Person Create(QueryablePerson person)
        {
            return new Person
            {
                Id = person.Id,
                Title = person.Title,
                Firstname = person.Firstname,
                MiddleName = person.MiddleName,
                Surname = person.Surname,
                PreferredFirstname = person.PreferredFirstname,
                PreferredSurname = person.PreferredSurname,
                Ethinicity = person.Ethinicity,
                Nationality = person.Nationality,
                PlaceOfBirth = person.PlaceOfBirth,
                DateOfBirth = person.DateOfBirth,
                Gender = person.Gender,
                Identification = Create(person.Identification ?? new List<ES.Identification>()),
                PersonTypes = person.PersonTypes,
                IsPersonCautionaryAlert = person.IsPersonCautionaryAlert,
                IsTenureCautionaryAlert = person.IsTenureCautionaryAlert,
                Tenures = Create(person.Tenures ?? new List<ES.Tenures>())
            };
        }

        private static List<Identification> Create(List<ES.Identification> identifications)
        {
            var identList = new List<Identification>();

            foreach (ES.Identification identification in identifications)
            {
                identList.Add(new Identification
                {
                    IdentificationType = identification.IdentificationType,
                    LinkToDocument = identification.LinkToDocument,
                    OriginalDocumentSeen = identification.OriginalDocumentSeen,
                    Value = identification.Value
                });
            }

            return identList;
        }

        private static List<Tenures> Create(List<ES.Tenures> tenures)
        {
            var tenureList = new List<Tenures>();

            foreach (ES.Tenures tenure in tenures)
            {
                tenureList.Add(new Tenures
                {
                    AssetFullAddress = tenure.AssetFullAddress,
                    EndDate = tenure.EndDate,
                    Id = tenure.Id,
                    StartDate = tenure.StartDate,
                    Type = tenure.Type
                });
            }

            return tenureList;
        }

        public string Id { get; set; }

        public string Title { get; set; }

        public string Firstname { get; set; }

        public string MiddleName { get; set; }

        public string Surname { get; set; }

        public string PreferredFirstname { get; set; }

        public string PreferredSurname { get; set; }

        public string Ethinicity { get; set; }

        public string Nationality { get; set; }

        public string PlaceOfBirth { get; set; }

        public string DateOfBirth { get; set; }

        public string Gender { get; set; }

        public List<Identification> Identification { get; set; }

        public List<string> PersonTypes { get; set; }

        public bool IsPersonCautionaryAlert { get; set; }

        public bool IsTenureCautionaryAlert { get; set; }

        public List<Tenures> Tenures { get; set; }
    }
}
