namespace TeamUp.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using TeamUp.Data;
    using TeamUp.Models;
    using TeamUp.Web.Models.Messages;

    public class MessagesController : BaseAuthorizeController
    {
        public MessagesController(ITeamUpData data)
            : base(data)
        {

        }

        // GET: Index
        public ActionResult Index()
        {
            var model = this.CurrentUser.MyMessages
                        .OrderBy(m => m.IsRead)
                        .ThenByDescending(m => m.DateSent)
                        .AsQueryable()
                        .Project().To<ReadMessageViewModel>();

            return View(model);
        }

        // GET: Messages/Send/{id}
        public ActionResult Send(string id)
        {
            var recipient = this.Data.Users.GetById(id);
            if (recipient == null)
            {
                return RedirectToAction("Index", "Users");
            }

            var model = new MessageViewModel();
            model.RecipientId = id;
            model.Recipient = recipient;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Send(MessageViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.RecipientId != this.CurrentUser.Id)
                {
                    var dbMessage = Mapper.Map<Message>(model);
                    dbMessage.AuthorId = this.CurrentUser.Id;
                    dbMessage.Author = this.CurrentUser;
                    this.Data.Messages.Add(dbMessage);
                    this.Data.Users.GetById(model.RecipientId).MyMessages.Add(dbMessage);
                    this.Data.SaveChanges();
                    TempData["SentSuccess"] = "Message successfully sent!";
                    return RedirectToAction("Details", "Users", new { id = model.RecipientId });
                }
                else
                {
                    ModelState.AddModelError("MessageToSelf", "You cannot send a message to yourself");
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult MarkAsRead(int id)
        {
            if (Request.IsAjaxRequest())
            {
                var messageInDb = this.Data.Messages.GetById(id);
                if (!messageInDb.IsRead)
                {
                    messageInDb.IsRead = true;
                    this.Data.SaveChanges();
                }

                return new EmptyResult();
            }

            return RedirectToAction("Index");
        }
    }
}