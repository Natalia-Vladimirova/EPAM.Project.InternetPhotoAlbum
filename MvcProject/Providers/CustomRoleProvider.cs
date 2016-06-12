using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using BLL.Interfaces.Entities;
using BLL.Interfaces.Services;

namespace MvcProject.Providers
{
    public class CustomRoleProvider : RoleProvider
    {
        public IUserService UserService
            => (IUserService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IUserService));

        public IRoleService RoleService
            => (IRoleService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IRoleService));
        
        public override bool IsUserInRole(string login, string roleName)
        {
            var user = UserService.GetUserEntityByLogin(login);

            if (user == null) return false;

            IEnumerable<RoleEntity> userRoles = RoleService.GetUserRoleEntities(user.Id);

            if (userRoles == null) return false;

            if (userRoles.Any(role => role.RoleName == roleName))
            {
                return true;
            }

            return false;
        }

        public override string[] GetUsersInRole(string roleName)
        {
            return RoleService.GetUserEntitiesInRole(roleName)?.Select(user => user.Login).ToArray();
        }

        public override string[] GetRolesForUser(string login)
        {
            var user = UserService.GetUserEntityByLogin(login);
            if (user == null) return null;

            return RoleService.GetUserRoleEntities(user.Id)?.Select(role => role.RoleName).ToArray();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName { get; set; }
    }
}