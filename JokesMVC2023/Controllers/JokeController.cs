﻿using JokesMVC2023.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JokesMVC2023.Models.Data;

namespace JokesMVC2023.Controllers
{
    public class JokeController : Controller
    {
        private readonly JokeDBContext _jokeContext;
        public JokeController(JokeDBContext jokeContext)
        {
            _jokeContext = jokeContext;
        }

        // GET: JokeController
        public ActionResult Index()
        {
            return View(_jokeContext.Jokes.AsEnumerable());
        }

        /*
         * The method below will be used by a fetch request from the client, to return an updated
         * table of joke data - this will be retruned as HTML from this method to the fetch client,
         * and will then be rendered in the browser via javascript
         */

        [HttpGet]
        public async Task<ActionResult> JokeTablePartial(string query = "")
        {
            var jokeList = _jokeContext.Jokes.AsQueryable();

            if (!String.IsNullOrEmpty(query))
            {
                jokeList = jokeList.Where(c => c.JokeQuestion.Contains(query) || c.JokeAnswer.Contains(query));
            }

            return PartialView("_JokeTable", jokeList.ToList());
        }

        /*
         * The pair of create methods below are both utilised by a fetch client to first retrieve 
         * the HTML required to display an empty create form.
         * The second method will accept a POST request containing new joke data. the [FromBody] was required
         * to allow the endpoint to correctly parse and bind the content from the fetch POST
         */


        public async Task<ActionResult> Create()
        {
            return PartialView("_Create");
        }

        // POST: JokeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromBody] JokeCreateDTO jokeCreate)
        {
            try
            {
                // simple error handling
                if (ModelState.IsValid)
                {
                    Thread.Sleep(2000);
                    _jokeContext.Jokes.Add(new Joke { JokeQuestion = jokeCreate.JokeQuestion, JokeAnswer = jokeCreate.JokeAnswer });
                    _jokeContext.SaveChanges();
                    return Created("/Joke/Create", jokeCreate);
                }
                else
                {
                    return BadRequest("Validation Failed");
                }
            }
            catch
            {
                return Problem("An error has occured");
            }
        }



        // GET: JokeController/Details/5
        public async Task<ActionResult> Details([FromQuery] int id)
        {
            if(id == 0)
            {
                return RedirectToAction(nameof(Index));
            }

            var joke = _jokeContext.Jokes.FirstOrDefault(c => c.Id == id);

            return joke != null ? PartialView("_Details", joke) : RedirectToAction(nameof(Index));
        }





        // GET: JokeController/Edit/5
        public async Task<ActionResult> EditForm(int id)
        {
            if (id == 0)
            {
                return RedirectToAction(nameof(Index));
            }

            var joke = _jokeContext.Jokes.FirstOrDefault(c => c.Id == id);

            return joke != null ? PartialView("_Edit", joke) : RedirectToAction(nameof(Index));
        }

        // POST: JokeController/Edit/5
        [HttpPut]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([FromQuery]int id, [FromBody]Joke jokeEdit)
        {
            //if (id != jokeEdit.Id)
            //{
            //    return NotFound();
            //}

            if (ModelState.IsValid)
            {
                _jokeContext.Jokes.Update(jokeEdit);
                _jokeContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(jokeEdit);
        }

       

        // POST: JokeController/Delete/5
        [HttpDelete]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                var joke = _jokeContext.Jokes.FirstOrDefault(c => c.Id == id);
                if (joke != null)
                {
                    //Thread.Sleep(3000);
                    _jokeContext.Jokes.Remove(joke);
                    _jokeContext.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch
            {
                return View();
            }
        }
    }
}
