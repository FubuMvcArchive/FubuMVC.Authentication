using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Web;
using FubuMVC.Authentication.OAuth;

namespace FubuMVC.Authentication.Facebook
{
    [DataContract]
    public class FacebookGraph : IOAuthGraph
    {
        private static readonly DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof (FacebookGraph));

        [DataMember(Name = "id")]
        public long Id { get; set; }

        [DataMember(Name = "first_name")]
        public string FirstName { get; set; }

        [DataMember(Name = "last_name")]
        public string LastName { get; set; }

        [DataMember(Name = "email")]
        public string Email { get; set; }

        [DataMember(Name = "verified")]
        public string Verified { get; set; }

        [DataMember(Name = "link")]
        public Uri Link { get; set; }

        public OAuthResponse Deserialize(Stream jsonStream)
        {
            if (jsonStream == null)
            {
                throw new ArgumentNullException("jsonStream");
            }

            var graph = (FacebookGraph) jsonSerializer.ReadObject(jsonStream);

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