using BestPowerShoesBLL;
using BestPowerShoesEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace BestPowerShoes.WebApi.Controllers
{
    public class PartyController : ApiController
    {
        public IHttpActionResult GetParties() 
        {
            var lstparties = new PartyBLL().GetParties();
            try
            {
                if (lstparties!=null )
                {
                    return Ok(lstparties);

                }
            }
            catch (Exception)
            {
                
                throw;
            }
            return BadRequest();

        }


        public IHttpActionResult SaveParty(Party p) 
        {            
            try
            {
                string Msg = "";
                var lstparties = new PartyBLL().SaveParty(p);

                if (Convert.ToInt32(lstparties) ==0)
                {
                    Msg = "Warning: Error occur while excuting, Please Insert correct Information";
                }
                else
                {
                    Msg = "Conratulations: Party Added Successfully!";
                }

                return Ok(Msg);
              
            }
            catch (Exception)
            {
                
                throw;
            }
            return BadRequest();

        }
    }
}
