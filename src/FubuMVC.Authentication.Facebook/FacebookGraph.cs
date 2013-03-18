using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Web;
using FubuMVC.Authentication.OAuth2;

namespace FubuMVC.Authentication.Facebook
{
    [DataContract]
    public class FacebookGraph : IOAuth2Graph
    {
        private static readonly DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(FacebookGraph));

        [DataMember(Name = "id")]
        public long Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "first_name")]
        public string FirstName { get; set; }

        [DataMember(Name = "last_name")]
        public string LastName { get; set; }

        [DataMember(Name = "link")]
        public Uri Link { get; set; }

        [DataMember(Name = "birthday")]
        public string Birthday { get; set; }

        [DataMember(Name = "email")]
        public string Email { get; set; }

        [DataMember(Name = "verified_email")]
        public bool Verified { get; set; }

        public OAuth2Response Deserialize(Stream jsonStream)
        {
            if (jsonStream == null)
            {
                throw new ArgumentNullException("jsonStream");
            }

            var graph = (FacebookGraph)jsonSerializer.ReadObject(jsonStream);
            var response = new OAuth2Response
                {
                    UserId = Get(graph.Id),
                    Name = Get(graph.Name),
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