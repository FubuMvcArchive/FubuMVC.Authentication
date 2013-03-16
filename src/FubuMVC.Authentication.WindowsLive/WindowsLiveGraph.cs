using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Web;
using System.Web.Script.Serialization;
using FubuMVC.Authentication.OAuth2;

namespace FubuMVC.Authentication.WindowsLive
{
    [DataContract]
    public class WindowsLiveGraph : IOAuth2Graph
    {
        private static readonly DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof (WindowsLiveGraph));
        private static readonly JavaScriptSerializer serializer = new JavaScriptSerializer();

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "first_name")]
        public string FirstName { get; set; }

        [DataMember(Name = "last_name")]
        public string LastName { get; set; }

        [DataMember(Name = "link")]
        public Uri Link { get; set; }

        [DataMember(Name = "gender")]
        public string Gender { get; set; }

        [DataMember(Name = "updated_time")]
        public string UpdatedTime { get; set; }

        [DataMember(Name = "locale")]
        public string Locale { get; set; }

        [DataMember(Name = "emails")]
        public Dictionary<string, string> Emails { get; set; }

        [DataMember(Name = "verified_email")]
        public bool Verified { get; set; }

        public OAuth2Response Deserialize(Stream jsonStream)
        {
            if (jsonStream == null)
            {
                throw new ArgumentNullException("jsonStream");
            }

            var graph = serializer.Deserialize<WindowsLiveGraph>(new StreamReader(jsonStream).ReadToEnd());

            var response = new OAuth2Response
                {
                    UserId = Get(graph.Id),
                    Name = Get(graph.Name),
                    FirstName = Get(graph.FirstName),
                    LastName = Get(graph.LastName),
                    Email = Get(graph.Emails["preferred"] ?? graph.Emails["account"]),
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