﻿#region license

// http://www.gnu.org/licenses/gpl-3.0.html GPL v3 or later

#endregion

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Piwik.Analytics.NetCore.Parameters;

/// <summary>
/// Piwik - Open source web analytics
/// For more information, see http://piwik.org
/// </summary>

namespace Piwik.Analytics.NetCore.Modules
{
    public class SiteIdResult
    {
        [JsonProperty(SitesManager.ID)]
        public int SiteId { get; set; }
    }
    
    public class SiteInfoResult
    {
        [JsonProperty(SitesManager.ID)]
        public int SiteId { get; set; }
        
        [JsonProperty(SitesManager.NAME)]
        public string Name { get; set; }
        
        [JsonProperty(SitesManager.MAIN_URL)]
        public string MainUrl { get; set; }
        
        [JsonProperty(SitesManager.TS_CREATED)]
        public string Created { get; set; }
        
        [JsonProperty(SitesManager.TIMEZONE)]
        public string Timezone { get; set; }
        
        [JsonProperty(SitesManager.CURRENCY)]
        public string Currency { get; set; }
        
        //TODo: complete class
    }
    
    
    /// <summary>
    ///     Service Gateway for Piwik SitesManager Module API
    ///     For more information, see http://piwik.org/docs/analytics-api/reference
    /// </summary>
    /// <remarks>
    ///     This Analytics API is tested against Piwik 1.5
    /// </remarks>
    public class SitesManager : PiwikAnalytics
    {
        public const string ID = "idsite";
        public const string NAME = "name";
        public const string MAIN_URL = "main_url";
        public const string TS_CREATED = "ts_created";
        public const string TIMEZONE = "timezone";
        public const string CURRENCY = "currency";
        public const string EXCLUDED_IPS = "excluded_ips";
        public const string EXCLUDED_PARAMETERS = "excluded_parameters";
        public const string FEEDBURNER_NAME = "feedburnerName";
        public const string GROUP = "group";
        public const string ECOMMERCE = "ecommerce";

        private const string PLUGIN = "SitesManager";

        protected override string GetPlugin()
        {
            return PLUGIN;
        }

        /// <summary>
        ///     Add a piwik site
        /// </summary>
        /// <param name="siteName"></param>
        /// <param name="urls"></param>
        /// <param name="ecommerce"></param>
        /// <param name="excludedIps"></param>
        /// <param name="excludedQueryParameters"></param>
        /// <param name="timezone"></param>
        /// <param name="currency"></param>
        /// <param name="group"></param>
        /// <param name="startDate"></param>
        /// <returns></returns>
        public int AddSite(
            string siteName,
            string[] urls,
            bool ecommerce = false,
            string[] excludedIps = null,
            string[] excludedQueryParameters = null,
            string timezone = null,
            string currency = null,
            string group = null,
            DateTimeOffset startDate = default(DateTimeOffset))
        {
            Parameter[] parameters =
            {
                new SimpleParameter("siteName", siteName),
                new SimpleParameter("ecommerce", ecommerce),
                new ArrayParameter("excludedIps", excludedIps, true),
                new ArrayParameter("excludedQueryParameters", excludedQueryParameters, true),
                new ArrayParameter("urls", urls),
                new SimpleParameter("timezone", timezone),
                new SimpleParameter("currency", currency),
                new SimpleParameter("group", group),
                new DateParameter("startDate", startDate)
            };

            return SendRequest<int>("addSite", parameters);
        }

        /// <summary>
        ///     Remove a Piwik site
        /// </summary>
        /// <param name="idSite"></param>
        /// <returns></returns>
        public bool DeleteSite(int idSite)
        {
            Parameter[] parameters =
            {
                new SimpleParameter("idSite", idSite)
            };

            return SendRequest<bool>("deleteSite", parameters);
        }

        /// <summary>
        ///     Find sites from their URL
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public List<SiteIdResult> GetSitesIdFromSiteUrl(string url)
        {
            Parameter[] parameters =
            {
                new SimpleParameter("url", url)
            };

            return SendRequest<List<SiteIdResult>>("getSitesIdFromSiteUrl", parameters);
        }
        
        public List<Uri> GetSiteUrlsFromId(int siteId)
        {
            var parameter = new SimpleParameter("idSite", siteId);
            
            return SendRequest<List<Uri>>("getSiteUrlsFromId", parameter);
        }

        /// <summary>
        ///     Find a site from its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<SiteInfoResult> GetSiteFromId(int id)
        {
            var parameter = new SimpleParameter("idSite", id);

            return SendRequest<List<SiteInfoResult>>("getSiteFromId", parameter);
        }

        /// <summary>
        ///     Update a Piwik Site.
        ///     All the existing parameters need to be provided otherwise they will be erased from the database.
        /// </summary>
        /// <param name="idSite"></param>
        /// <param name="siteName"></param>
        /// <param name="urls"></param>
        /// <param name="ecommerce"></param>
        /// <param name="excludedIps"></param>
        /// <param name="excludedQueryParameters"></param>
        /// <param name="timezone"></param>
        /// <param name="currency"></param>
        /// <param name="group"></param>
        /// <param name="startDate"></param>
        /// <returns></returns>
        public bool UpdateSite(
            int idSite,
            string siteName,
            string[] urls,
            bool ecommerce = false,
            string[] excludedIps = null,
            string[] excludedQueryParameters = null,
            string timezone = null,
            string currency = null,
            string group = null,
            DateTimeOffset startDate = default(DateTimeOffset))
        {
            Parameter[] parameters =
            {
                new SimpleParameter("idSite", idSite),
                new SimpleParameter("siteName", siteName),
                new SimpleParameter("ecommerce", ecommerce),
                new ArrayParameter("excludedIps", excludedIps, true),
                new ArrayParameter("excludedQueryParameters", excludedQueryParameters, true),
                new ArrayParameter("urls", urls),
                new SimpleParameter("timezone", timezone),
                new SimpleParameter("currency", currency),
                new SimpleParameter("group", group),
                new DateParameter("startDate", startDate)
            };

            return SendRequest<bool>("updateSite", parameters);
        }
    }
}