using Numbers.IService;
using Numbers.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyWebApp.Controllers.api
{
    [RoutePrefix("api/numbertowords")]
    public class NumberToWordsController : ApiController
    {
        [Route("{nbr}/{currency}")]
        [HttpGet]
        public string Convert(string nbr, bool currency = false)
        {
            nbr = nbr.Replace("_", ".");
            IUtils loUtils = new Utils();
            string lsWords = string.Empty;

            loUtils.NumberToWords(nbr, out lsWords, currency);
            return lsWords.ToUpper();
        }
    }
}
