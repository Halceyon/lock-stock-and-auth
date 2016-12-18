using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sample_MVC_Site.Controllers.Api
{
    [Authorize]
    [RoutePrefix("api/books")]
    public class BooksController : ApiController
    {
        // GET: api/Books
        public IEnumerable<Book> Get()
        {
            return new List<Book>()
            {
                new Book()
                {
                    Id = 1,
                    Name = "Moby Dick",
                    Author = "Herman Melville"
                },
                new Book()
                {
                    Id = 2,
                    Name = "Hamlet",
                    Author = "William Shakespeare"
                }
            };
        }

        // GET: api/Books/5
        public Book Get(int id)
        {
            return new Book()
            {
                Id = 1,
                Name = "Moby Dick",
                Author = "Herman Melville"
            };
        }

        // POST: api/Books
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Books/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Books/5
        public void Delete(int id)
        {
        }

        public class Book
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Author { get; set; }
        }
    }
}
