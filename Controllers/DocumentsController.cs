using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace testdocsapi.Controllers
{
    // uncomment EnableCors to restrict access 
    [EnableCors("AllowSpecificOrigin")]
    [Route("api/[controller]")]
    public class DocumentsController : Controller
    {
                       
        // [Authorize]
        [HttpGet]
        public IActionResult Get()
        {               
            var result = DocumentRepo.GetDocuments();   
            return new ObjectResult(result);
        }
    }

    public class Document
    {
        public int id { get; set; }
        public string institution { get; set; }
    }

    public static class DocumentRepo
    {
        public static List<Document> GetDocuments()
        {
            List<Document> docs = new List<Document>(){
                new Document(){ id = 9496, institution = "Richter & Jeter Ltd"},
                new Document(){ id = 6391, institution = "Gilmour and Waters"},
                new Document(){ id = 2487, institution = "Wynwood Ventures"},
                new Document(){ id = 427, institution = "56 Hope Road Productions"},
                new Document(){ id = 892, institution = "The Cobb Group"},
                new Document(){ id = 6771, institution = "Indus, Dal & Anchar"}    
            };

            return docs;
        }
    }
}

