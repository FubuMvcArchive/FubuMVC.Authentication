using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Web;
using FubuMVC.Authentication.OAuth;

namespace FubuMVC.Authentication.Google
{
    [DataContract]
    public class GoogleGraph : IOAuthGraph
    {
        private static readonly DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(GoogleGraph));

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "given_name")]
        public string FirstName { get; set; }

        [DataMember(Name = "family_name")]
        public string LastName { get; set; }

        [DataMember(Name = "link")]
        public Uri Link { get; set; }

        [DataMember(Name = "email")]
        public string Email { get; set; }

        [DataMember(Name = "verified_email")]
        public bool Verified { get; set; }

        public OAuthResponse Deserialize(Stream jsonStream)
        {
            if (jsonStream == null)
            {
                throw new ArgumentNullException("jsonStream");
            }

            var graph = (GoogleGraph)jsonSerializer.ReadObject(jsonStream);
            var response = new OAuthResponse
                {
                    UserId = Get(graph.Id),
                    FirstName = Get(graph.FirstName),
                    LastName = Get(graph.LastName),
                    Email = Get(graph.Email),
                    EmailVerified = bool.Parse(Get(graph.Verified)),
                    Url = Get(graph.Link)
                };
            return response;
        }

        public static string Get(object itemToEncode)
        {
            return HttpUtility.HtmlEncode(itemToEncode);
        }
    }
}