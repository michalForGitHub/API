using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.WebHost;
using System.Web.Routing;
using System.Web.SessionState;
using System.Web.Http.Cors;
using Repository.Models;

namespace Repository.Controllers
{


    [RoutePrefix("Api/RepositorySearch")]
    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "X-Custom-Header")]
    public class RepositoryController : ApiController
    {
        /// <summary>
        /// bookmark a repository.
        /// </summary>
        /// <param name="json"></param>
        /// <returns>ok</returns>
        [HttpPost]
        [Route("starBookmarkRepository")]
        public IHttpActionResult starBookmarkRepository(SearchResult json)
        {
            IEnumerable<string> headerValues = HttpContext.Current.Request.Headers.GetValues("guid");
            var guid = headerValues.FirstOrDefault();
            var cache = HttpContext.Current.Cache;
            List<SearchResult> staredList;
            if (cache != null)
            {
                if (cache["stars_" + guid] == null)
                {
                    staredList = new List<SearchResult>();
                    staredList.Add(json);
                    cache["stars_" + guid] = staredList;
                }
                else
                {
                    staredList = cache["stars_" + guid] as List<SearchResult>;
                    bool containsItem = staredList.Any(item => item.ID == json.ID);
                    if (!containsItem)
                    {
                        staredList.Add(json);
                        cache["stars_" + guid] = staredList;
                    }
                }
            }
            return Ok(json);
        }
        /// <summary>
        /// Remove a bookmark from the repository.
        /// </summary>
        /// <param name="json"></param>item to remove
        /// <returns>ok</returns>
        [HttpPost]
        [Route("unstarBookmarkRepository")]
        public IHttpActionResult unstarBookmarkRepository(SearchResult json)
        {
            IEnumerable<string> headerValues = HttpContext.Current.Request.Headers.GetValues("guid");
            var guid = headerValues.FirstOrDefault();
            var cache = HttpContext.Current.Cache;
            List<SearchResult> staredList;
            if (cache != null)
            {
                if (cache["stars_" + guid] != null)
                {
                    staredList = cache["stars_" + guid] as List<SearchResult>;
                    var containsItem = staredList.Where(item => item.ID == json.ID).FirstOrDefault();
                    if(containsItem!=null)
                    {
                        staredList.Remove(containsItem);
                        cache["stars_" + guid] = staredList;
                    }
                }  
            }
            return Ok(json);
        }
        /// <summary>
        /// get all repository per  browser session 
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetBookmarkResult/{guid}")]
        public List<SearchResult> GetBookmarkResult(string guid)
        {
            var cache = HttpContext.Current.Cache;

            if (cache != null)
            {
                if (cache["stars_" + guid] != null)
                {
                    return cache["stars_" + guid] as List<SearchResult>;
                }
            }
            return null;
        }
    }
}