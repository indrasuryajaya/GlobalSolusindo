﻿using GlobalSolusindo.Identity.User;
using GlobalSolusindo.Identity.User.DML;
using GlobalSolusindo.Identity.User.EntryForm;
using GlobalSolusindo.Identity.User.Queries;
using Kairos;
using Kairos.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Transactions;
using System.Web.Http;

namespace GlobalSolusindo.Api.Controllers
{
    public class UserController : ApiControllerBase
    {
        public UserController()
        {
        }

        [Route("user/{id}")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            string accessType = "User_ViewAll";
            ThrowIfUserCannotAccess(accessType);
            using (UserQuery userQuery = new UserQuery(Db))
            {
                var data = userQuery.GetByPrimaryKey(id);
                SaveLog("User", "Get", JsonConvert.SerializeObject(new { primaryKey = id }));
                return Ok(new SuccessResponse(data));
            }
        }

        [Route("user/form/{id}")]
        [HttpGet]
        public IHttpActionResult GetForm(int id)
        {
            string accessType = "User_ViewAll";
            ThrowIfUserCannotAccess(accessType);
            using (UserEntryDataProvider userEntryDataProvider = new UserEntryDataProvider(Db, ActiveUser, AccessControl, new UserQuery(Db)))
            {
                var data = userEntryDataProvider.Get(id);
                SaveLog("User", "GetForm", JsonConvert.SerializeObject(new { primaryKey = id }));
                return Ok(new SuccessResponse(data));
            }
        }

        [Route("user/search")]
        [HttpGet]
        public IHttpActionResult Search([FromUri]UserSearchFilter filter)
        {
            string accessType = "User_ViewAll";
            ThrowIfUserCannotAccess(accessType);
            if (filter == null)
                throw new KairosException("Missing search filter parameter");

            using (var userSearch = new UserSearch(Db))
            {
                var data = userSearch.GetDataByFilter(filter);
                return Ok(new SuccessResponse(data));
            }
        }

        [Route("user")]
        [HttpPost]
        public IHttpActionResult Create([FromBody]UserDTO user)
        {
            string accessType = "";
            ThrowIfUserCannotAccess(accessType);
            if (user == null)
                throw new KairosException("Missing model parameter");

            if (user.User_PK != 0)
                throw new KairosException("Post method is not allowed because the requested primary key is must be '0' (zero) .");
            using (var userCreateHandler = new UserCreateHandler(Db, ActiveUser, new UserValidator(), new UserFactory(Db, ActiveUser), new UserQuery(Db), AccessControl))
            {
                using (var transaction = new TransactionScope())
                { 
                    var saveResult = userCreateHandler.Save(userDTO: user, dateStamp: DateTime.UtcNow);
                    transaction.Complete();
                    if (saveResult.Success)
                        return Ok(new SuccessResponse(saveResult.Model, saveResult.Message));
                    return Ok(new ErrorResponse(ServiceStatusCode.ValidationError, saveResult.ValidationResult, saveResult.Message));
                }
            }
        }

        [Route("user")]
        [HttpPut]
        public IHttpActionResult Update([FromBody]UserDTO user)
        {
            string accessType = "";
            ThrowIfUserCannotAccess(accessType);
            if (user == null)
                throw new KairosException("Missing model parameter");

            if (user.User_PK == 0)
                throw new KairosException("Put method is not allowed because the requested primary key is '0' (zero) .");

            using (var userUpdateHandler = new UserUpdateHandler(Db, ActiveUser, new UserValidator(), new UserFactory(Db, ActiveUser), new UserQuery(Db), AccessControl))
            {
                using (var transaction = new TransactionScope())
                {
                    var saveResult = userUpdateHandler.Save(user, DateTime.UtcNow);
                    transaction.Complete();
                    if (saveResult.Success)
                        return Ok(new SuccessResponse(saveResult.Model, saveResult.Message));
                    return Ok(new ErrorResponse(ServiceStatusCode.ValidationError, saveResult.ValidationResult, saveResult.Message));
                }
            }
        }

        [Route("user")]
        [HttpDelete]
        public IHttpActionResult Delete([FromBody] List<int> ids)
        {
            if (ids == null)
                throw new KairosException("Missing parameter: 'ids'");

            string accessType = "";
            ThrowIfUserCannotAccess(accessType);

            using (var userDeleteHandler = new UserDeleteHandler(Db, ActiveUser))
            {
                using (var transaction = new TransactionScope())
                {
                    var result = new List<DeleteResult<int>>();

                    foreach (var id in ids)
                    {
                        result.Add(userDeleteHandler.Execute(id, Base.DeleteMethod.Soft));
                    }
                    transaction.Complete();
                    return Ok(new SuccessResponse(result));
                }
            }
        }
    }
}
