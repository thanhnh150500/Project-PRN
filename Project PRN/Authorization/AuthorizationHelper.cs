using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_PRN.ExceptionHandler;
using Project_PRN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_PRN.Authorization
{
    public class AuthorizationHelper
    {
        static PRN211_TechnologyNewsContext db = new PRN211_TechnologyNewsContext();

        //Test case:
        //TC-1: false-controller || false-action --> define 404 by route.
        //TC-2: right-controller && right-action
        //TC-2.1: false-account(session) --> throw exception
        //TC-2.2: true-account && false-permission(db) --> return false
        //TC-2.2: true-account && true-permission(db) --> return true
        public static bool IsValidAccess(Account account, string ControllerName, string ActionName)
        {
            Guard.ThrowIfNull(account, "Account can't be null at AuthorizationHelper/IsValidAccess");

            #region curController
            Models.Controller curController =
                db //DB context.
                .Controllers //Table Controllers.
                .Include(obj => obj.Actions) //Disnable lazy load pattern of EF
                                             //ref: https://stackoverflow.com/questions/24022957/entity-framework-how-to-disable-lazy-loading-for-specific-query
                .ToList() //To backend Collection.
                .Find( //Filter Collection.
                    obj => //Parameter is each Item in Collection
                    obj
                    .ControllerName
                    .Equals(
                        ControllerName
                     )
                 );
            #endregion

            #region curAction
            Models.Action curAction =
                curController
                .Actions //Controller model 1 -n Action model.
                .ToList() //To backend Collection.
                .Find( //Filter Collection.
                    obj => //Parameter is each Item in Collection
                    obj
                    .ActionName
                    .Equals(
                        ActionName
                    )
                    &&
                    obj.ControllerId.Equals(curController.ControllerId) //This condition extra cuz lazy load
                );
            #endregion



            #region curRole
            int roleId = account.RoleId;

            //ref: https://stackoverflow.com/questions/15577890/how-to-use-lambda-in-linq-select-statement
            Role curRole =
                db.Roles
                .Include(obj => obj.RoleActions)
                //RoleId from account of session.
                .Where(obj => obj.Id.Equals(roleId))
                .Select(item => new Role { Id = item.Id, Title = item.Title })
                .FirstOrDefault();
            #endregion

            #region currentRoleAction
            RoleAction currentRoleAction = null;
            //Don't have permission to access it (with all account).
            if (curAction != null)
            {
                currentRoleAction = new RoleAction
                {
                    ActionId = curAction.ActionId,
                    RoleId = curRole.Id // roleId
                };
            }
            #endregion

            //Don't have permission to access it (with some type of account) when return false.
            return db.RoleActions.Contains(currentRoleAction);
        }
    }
}
